using UnityEngine;
using World;

namespace AsteroidGame.Bullets
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _bulletSpeed;

        public Bullet Make()
        {
            var bullet = CreateBullet();
            bullet.FlightDistance = WorldBounds.Instance.Width;
            bullet.Speed = _bulletSpeed;
            
            return bullet;
        }

        private Bullet CreateBullet() => Instantiate(_bulletPrefab, transform);
    }
}
