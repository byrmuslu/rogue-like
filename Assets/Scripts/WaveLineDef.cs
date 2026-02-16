using System;
using UnityEngine;

namespace Base.Manager
{
    public sealed class WaveLineDef
    {
        public int Count;            // toplam kaç düşman
        public float Interval;       // her spawn arası saniye
        public float SpreadRadius;   // oyuncu etrafına dağılım yarıçapı
        public Func<World, IEntity, Vector2, IEntity> Factory; // düşman üretimi

        public WaveLineDef(int count, float interval, float spreadRadius,
            Func<World, IEntity, Vector2, IEntity> factory)
        {
            Count = count;
            Interval = interval;
            SpreadRadius = spreadRadius;
            Factory = factory;
        }
    }
}