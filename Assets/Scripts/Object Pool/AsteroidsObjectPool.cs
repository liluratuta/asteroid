using System.Collections.Generic;
using AsteroidGame.Asteroids;
using UnityEngine;

namespace AsteroidGame.ObjectPools
{
    [RequireComponent(typeof(AsteroidFactory))]
    public class AsteroidsObjectPool : ObjectPool<Asteroid>
    {
        private AsteroidFactory _asteroidFactory;
        
        protected override List<Asteroid> MakePoolObjects(int capacity)
        {
            var asteroids = new List<Asteroid>(capacity);

            for (var i = 0; i < capacity; i++)
            {
                asteroids.Add(_asteroidFactory.Make());
            }

            return asteroids;
        }

        protected override void Awake()
        {
            _asteroidFactory = GetComponent<AsteroidFactory>();
            base.Awake();
        }
    }
}
