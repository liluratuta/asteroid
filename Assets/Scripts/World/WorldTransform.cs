using System;
using UnityEngine;

namespace World
{
    public class WorldTransform : MonoBehaviour
    {
        private Transform _transform;

        public Vector2 Position
        {
            get => _transform.position;
            set => SetPositionToWorld(value);
        }

        public Vector2 Forward => _transform.right;

        public float Scale
        {
            set => _transform.localScale = new Vector3(value, value, 1f);
        }

        public void Translate(Vector2 translation) => Position += translation;

        public void RightTranslate(float step) => Position += (Vector2)_transform.right * step;

        public void Rotate(float angle) => _transform.Rotate(Vector3.back, angle);

        private void Awake() => _transform = transform;

        private void SetPositionToWorld(Vector2 position)
        {
            if (WorldBounds.Instance.IsInside(position))
            {
                _transform.position = position;
                return;
            }

            var correctPosition = WorldBounds.Instance.ConvertToWorldBoundsPosition(position);
            _transform.position = correctPosition;
        }
    }
}
