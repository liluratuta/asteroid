using System;
using UnityEngine;
using World;

namespace Flying_Objects
{
    [RequireComponent(typeof(WorldTransform))]
    public class Mover : MonoBehaviour
    {
        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }
        
        [SerializeField] private Vector2 _velocity;
        private WorldTransform _worldTransform;

        private void Awake() => _worldTransform = GetComponent<WorldTransform>();

        private void Update() => _worldTransform.Position += _velocity * Time.deltaTime;
    }
}