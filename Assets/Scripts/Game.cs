using System;
using System.Collections;
using System.Collections.Generic;
using AsteroidGame.Asteroids;
using UnityEngine;

namespace AsteroidGame
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        private int _asteroidsNumber = 2;
        private float _spawnAsteroidsPauseTime = 2f;
        
        private void Start()
        {
            SpawnAsteroids();
            _asteroidSpawner.FlyingAsteroidsOver += OnFlyingAsteroidsOver;
        }

        private void OnFlyingAsteroidsOver()
        {
            StartCoroutine(SpawnAsteroidsDeferred(_spawnAsteroidsPauseTime));
        }

        private void SpawnAsteroids()
        {
            for (var i = 0; i < _asteroidsNumber; i++)
            {
                _asteroidSpawner.RandomSpawn(AsteroidSize.Big);
            }

            _asteroidsNumber++;
        }

        private IEnumerator SpawnAsteroidsDeferred(float pauseTime)
        {
            yield return new WaitForSeconds(pauseTime);
            SpawnAsteroids();
        }
    }
}