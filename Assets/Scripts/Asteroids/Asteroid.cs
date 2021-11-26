using System;
using AsteroidGame.Ships;
using UnityEngine;
using World;

namespace AsteroidGame.Asteroids
{
    [RequireComponent(typeof(WorldTransform))]
    [RequireComponent(typeof(Collider2D))]
    public class Asteroid : MonoBehaviour
    {
        public event Action<Asteroid> FlightCompleted;
        public AsteroidSpawner AsteroidSpawner { get; set; }
        public float Speed { get; set; }

        [Header("Asteroid Scales")]
        [SerializeField] private float _big = 1.5f;
        [SerializeField] private float _medium = 1f;
        [SerializeField] private float _small = 0.5f;
        
        private WorldTransform _worldTransform;
        private Collider2D _collider;
        private Vector2 _velocity = Vector2.zero;

        private AsteroidSize _size;

        public void Launch(Vector2 position, Vector2 direction, AsteroidSize asteroidSize)
        {
            _worldTransform.Position = position;
            
            _size = asteroidSize;
            ApplySize(_size);
            
            _velocity = direction * Speed;
            _collider.enabled = true;
        }

        public void ApplyDamage()
        {
            var subAsteroidSize = GetSubSize();

            if (subAsteroidSize != AsteroidSize.Zero)
            {
                SpawnSubAsteroids(subAsteroidSize);
            }
            
            CompleteFlight();
        }

        private void Awake()
        {
            _worldTransform = GetComponent<WorldTransform>();
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            _worldTransform.Translate(_velocity * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ship ship))
            {
                CompleteFlight();
                ship.ApplyDamage();
            }
        }

        private void CompleteFlight()
        {
            _collider.enabled = false;
            _velocity = Vector2.zero;
            FlightCompleted?.Invoke(this);
        }

        private void SpawnSubAsteroids(AsteroidSize subAsteroidSize)
        {
            var asteroidDirection = _velocity.normalized;
            var asteroidPosition = _worldTransform.Position;

            var speed = AsteroidSpawner.RangeSpeed.GetRandom();

            var subAsteroidDirection = Rotate(asteroidDirection, 45f * Mathf.Deg2Rad);
            AsteroidSpawner.Spawn(asteroidPosition, subAsteroidDirection, speed, subAsteroidSize);

            subAsteroidDirection = Rotate(asteroidDirection, -45f * Mathf.Deg2Rad);
            AsteroidSpawner.Spawn(asteroidPosition, subAsteroidDirection, speed, subAsteroidSize);
        }

        private static Vector2 Rotate(Vector2 original, float radian)
        {
            return new Vector2(
                original.x * Mathf.Cos(radian) - original.y * Mathf.Sin(radian),
                original.x * Mathf.Sin(radian) + original.y * Mathf.Cos(radian)
            );
        }

        private void ApplySize(AsteroidSize asteroidSize)
        {
            var scale = asteroidSize switch
            {
                AsteroidSize.Zero => 0,
                AsteroidSize.Small => _small,
                AsteroidSize.Medium => _medium,
                AsteroidSize.Big => _big,
                _ => throw new ArgumentOutOfRangeException(nameof(asteroidSize), asteroidSize, null)
            };
            
            _worldTransform.Scale = scale;
        }

        private AsteroidSize GetSubSize()
        {
            return (AsteroidSize) (Mathf.Clamp((int)_size - 1, (int)AsteroidSize.Zero, (int)AsteroidSize.Big));
        }
    }
}