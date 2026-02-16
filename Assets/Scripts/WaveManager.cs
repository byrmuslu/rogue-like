using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Manager
{
    public sealed class WaveManager
    {
        private readonly World _world;

        private readonly Queue<WaveDefinition> _queue = new();
        private ActiveWave _active;

        // Opsiyonel event'ler
        public event Action<int> WaveStarted;    // param: wave index (0-based)
        public event Action<int> WaveCompleted;

        private int _currentWaveIndex = -1;

        public WaveManager(World world)
        {
            _world = world;
        }

        public void Enqueue(WaveDefinition wave)
        {
            _queue.Enqueue(wave);
        }

        public void ClearAll()
        {
            _queue.Clear();
            _active = null;
            _currentWaveIndex = -1;
        }

        public void Update(float dt)
        {
            // aktif dalga yoksa sıradakini başlat
            if (_active == null)
            {
                if (_queue.Count == 0) return;

                var next = _queue.Dequeue();
                _active = new ActiveWave(next);
                _currentWaveIndex++;
                WaveStarted?.Invoke(_currentWaveIndex);
            }

            // aktif dalgayı işlet
            if (_active.Update(dt, _world))
            {
                // dalga bitti
                WaveCompleted?.Invoke(_currentWaveIndex);
                _active = null; // sıradaki dalgaya geçmek için boşalt
            }
        }

        // İç sınıf: dalga çalışma durumu
        private sealed class ActiveWave
        {
            private float _delayLeft;
            private readonly List<ActiveLine> _lines;

            public ActiveWave(WaveDefinition def)
            {
                _delayLeft = def.StartDelay;
                _lines = def.Lines.Select(l => new ActiveLine(l)).ToList();
            }

            // true => wave tamamlandı
            public bool Update(float dt, World world)
            {
                if (_delayLeft > 0f)
                {
                    _delayLeft -= dt;
                    return false;
                }

                // tüm hatları güncelle
                foreach (var line in _lines)
                    line.Update(dt, world);

                // tüm hatlar bitmişse wave bitti
                return _lines.All(l => l.Done);
            }

            private sealed class ActiveLine
            {
                private int _remaining;
                private float _interval;
                private float _timer;
                private float _spread;
                private readonly Func<World, IEntity, Vector2, IEntity> _factory;

                public bool Done => _remaining <= 0;

                public ActiveLine(WaveLineDef def)
                {
                    _remaining = Mathf.Max(0, def.Count);
                    _interval = Mathf.Max(0.01f, def.Interval);
                    _timer = 0f; // ilk spawn hemen olabilir (istersen _interval ile başlat)
                    _spread = Mathf.Max(0f, def.SpreadRadius);
                    _factory = def.Factory;
                }

                public void Update(float dt, World world)
                {
                    if (Done) return;
                    if (world.PrimaryPlayer == null || world.PrimaryPlayer.IsDestroyed) return;

                    _timer -= dt;
                    if (_timer > 0f) return;

                    // Spawn noktası: oyuncu etrafında random bir halka/daire
                    Vector2 center = world.PrimaryPlayer.Position;
                    Vector2 offset = _spread <= 0f
                        ? Vector2.zero
                        : UnityEngine.Random.insideUnitCircle.normalized * _spread;

                    Vector2 spawnPos = center + offset;

                    // Düşmanı üret
                    var enemy = _factory(world, world.PrimaryPlayer, spawnPos);
                    world.AddEntity(enemy);

                    _remaining--;
                    _timer = _interval;
                }
            }
        }
    }
}