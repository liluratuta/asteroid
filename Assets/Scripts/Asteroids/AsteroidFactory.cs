using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Asteroids
{
    public class AsteroidFactory : MonoBehaviour
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        public Asteroid Make()
        {
            var asteroid = CreateAsteroid();
            asteroid.AsteroidSpawner = _asteroidSpawner;

            return asteroid;
        }

        private Asteroid CreateAsteroid() => Instantiate(_asteroidPrefab, transform);
    }
}