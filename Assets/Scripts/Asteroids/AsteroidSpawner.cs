using System;
using AsteroidGame.ObjectPools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidGame.Asteroids
{
    [RequireComponent(typeof(AsteroidsObjectPool))]
    public class AsteroidSpawner : MonoBehaviour
    {
        public event Action FlyingAsteroidsOver;

        public RangeValue RangeSpeed => _rangeSpeed;
        
        [SerializeField] private RangeValue _rangeSpeed;
        
        private AsteroidsObjectPool _objectPool;
        private int _flyingAsteroids = 0;
        
        public void Spawn(Vector2 position, Vector2 direction, float speed, AsteroidSize asteroidSize)
        {
            var asteroid = _objectPool.Get();
            asteroid.FlightCompleted += OnAsteroidFlightCompleted;
            asteroid.Speed = speed;
            asteroid.Launch(position, direction, asteroidSize);

            _flyingAsteroids++;
        }

        public void RandomSpawn(AsteroidSize asteroidSize)
        {
            var randomDirection = new Vector2(Random.value, Random.value).normalized;
            var randomPosition = new Vector2(Random.Range(-20, 20), Random.Range(-10, 10));

            Spawn(randomPosition, randomDirection, _rangeSpeed.GetRandom(), asteroidSize);
        }

        private void Awake() => _objectPool = GetComponent<AsteroidsObjectPool>();

        private void OnAsteroidFlightCompleted(Asteroid asteroid)
        {
            asteroid.FlightCompleted -= OnAsteroidFlightCompleted;
            
            _objectPool.Reclaim(asteroid);
            _flyingAsteroids--;

            if (_flyingAsteroids <= 0)
            {
                FlyingAsteroidsOver?.Invoke();
            }
        }
    }
}
