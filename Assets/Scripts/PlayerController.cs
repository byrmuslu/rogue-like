using UnityEngine;

namespace Base.Manager
{
    public class PlayerController : IController
    {
        private readonly float _moveSpeed;

        public PlayerController(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Update(Character character, float dt)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            character.Position += new Vector2(h, v) * _moveSpeed * dt;
        }
    }
}