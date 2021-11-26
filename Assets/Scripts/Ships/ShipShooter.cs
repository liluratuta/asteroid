using AsteroidGame.Bullets;
using UnityEngine;
using World;

namespace AsteroidGame.Ships
{
    public class ShipShooter : MonoBehaviour
    {
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private Transform _shotPoint;
        
        private const int Frequency = 3;
        private float[] _shotMoments;
        private WorldTransform _worldTransform;

        private void Awake()
        {
            _worldTransform = GetComponent<WorldTransform>();
            _shotMoments = new float[Frequency];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsShootPossible)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _bulletSpawner.Spawn(_shotPoint.position, _worldTransform.Forward);
            RecordMoment();
        }

        private void RecordMoment()
        {
            for (var i = 1; i < Frequency; i++)
            {
                _shotMoments[i - 1] = _shotMoments[i];
            }

            _shotMoments[Frequency - 1] = Time.time;
        }

        private bool IsShootPossible => (Time.time - _shotMoments[0]) > 1f;
    }
}