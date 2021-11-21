using System;
using UnityEngine;

namespace World
{
    public class WorldTransform : MonoBehaviour
    {
        private Transform _transform;
        private WorldBounds _worldBounds;

        private void Awake() => _transform = transform;

        private void Start() => _worldBounds = WorldBounds.Instance;

        public Vector2 Position
        {
            get => _transform.position;
            set => SetPositionToWorld(value);
        }

        private void SetPositionToWorld(Vector2 position)
        {
            if (_worldBounds.IsInside(position))
            {
                _transform.position = position;
                return;
            }

            var correctPosition = _worldBounds.ConvertToWorldBoundsPosition(position);
            _transform.position = correctPosition;
        }
    }
}
