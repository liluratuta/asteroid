using AsteroidGame.ObjectPools;
using UnityEngine;

namespace AsteroidGame.Bullets
{
    [RequireComponent(typeof(BulletsObjectPool))]
    public class BulletSpawner : MonoBehaviour
    {
        private BulletsObjectPool _objectPool;

        public void Spawn(Vector2 position, Vector2 direction)
        {
            var bullet = _objectPool.Get();
            
            bullet.Launch(position, direction);
            bullet.FlightCompleted += OnBulletFlightCompleted;
        }
        
        private void Awake() => _objectPool = GetComponent<BulletsObjectPool>();

        private void OnBulletFlightCompleted(Bullet bullet)
        {
            bullet.FlightCompleted -= OnBulletFlightCompleted;
            _objectPool.Reclaim(bullet);
        }
    }
}
