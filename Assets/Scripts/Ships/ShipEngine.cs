using UnityEngine;
using World;

namespace AsteroidGame.Ships
{
    [RequireComponent(typeof(WorldTransform))]
    public class ShipEngine : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 180f;
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _acceleration = 1f;

        private Vector2 _velocity;
        private WorldTransform _worldTransform;

        private void Awake() => _worldTransform = GetComponent<WorldTransform>();

        private void Update()
        {
            ApplyAcceleration(accelerationDegree: Input.GetAxis("Vertical"));
            ApplyRotation(rotationDegree: Input.GetAxis("Horizontal"));
        }

        private void ApplyAcceleration(float accelerationDegree)
        {
            _velocity += _worldTransform.Forward * _acceleration * accelerationDegree * Time.deltaTime; 
            _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);
            _worldTransform.Translate(_velocity * Time.deltaTime);
        }

        private void ApplyRotation(float rotationDegree)
        {
            var rotationAngle = _rotationSpeed * rotationDegree * Time.deltaTime;
            _worldTransform.Rotate(rotationAngle);
        }
    }
}
