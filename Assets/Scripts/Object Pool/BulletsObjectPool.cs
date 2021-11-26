using System.Collections.Generic;
using AsteroidGame.Bullets;
using UnityEngine;

namespace AsteroidGame.ObjectPools
{
    [RequireComponent(typeof(BulletFactory))]
    public class BulletsObjectPool : ObjectPool<Bullet>
    {
        private BulletFactory _bulletFactory;
        
        protected override List<Bullet> MakePoolObjects(int capacity)
        {
            var bullets = new List<Bullet>(capacity);

            for (var i = 0; i < capacity; i++)
            {
                bullets.Add(_bulletFactory.Make());
            }

            return bullets;
        }

        protected override void Awake()
        {
            _bulletFactory = GetComponent<BulletFactory>();
            base.Awake();
        }
    }
}
