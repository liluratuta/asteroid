using System;
using AsteroidGame.Asteroids;
using UnityEngine;
using World;

namespace AsteroidGame.Bullets
{
    [RequireComponent(typeof(WorldTransform))]
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> FlightCompleted;

        public float FlightDistance { get; set; }
        public float Speed { get; set; }
        
        private WorldTransform _worldTransform;
        private float _coveredDistance;
        private Vector2 _velocity;

        public void Launch(Vector2 position, Vector2 direction)
        {
            _coveredDistance = 0;
            _velocity = direction * Speed;
            _worldTransform.Position = position;
        }

        private void Awake() => _worldTransform = GetComponent<WorldTransform>();

        private void Update()
        {
            if (IsFlightDistanceReached)
            {
                CompleteFlight();
                return;
            }

            Fly();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Asteroid asteroid))
            {
                CompleteFlight();
                asteroid.ApplyDamage();
            }
        }

        private void Fly()
        {
            var step = _velocity * Time.deltaTime;
            _worldTransform.Translate(step);
            _coveredDistance += step.magnitude;
        }

        private void CompleteFlight()
        {
            _velocity = Vector2.zero;
            FlightCompleted?.Invoke(this);
        }

        private bool IsFlightDistanceReached => _coveredDistance >= FlightDistance;
    }
}
