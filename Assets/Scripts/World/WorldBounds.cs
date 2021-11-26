using System;
using UnityEngine;

namespace World
{
    public class WorldBounds : Singleton<WorldBounds>
    {
        public float Width => _area.width;
        
        private Camera _camera;
        private Rect _area;
        
        public bool IsInside(Vector2 position) => _area.Contains(position);

        public Vector2 ConvertToWorldBoundsPosition(Vector2 position)
        {
            var offset = _area.min - _area.center;

            var x = Mathf.Repeat(position.x - offset.x, _area.width);
            var y = Mathf.Repeat(position.y - offset.y, _area.height);

            return (new Vector2(x, y) + offset);
        }

        protected override void Initialize()
        {
            _camera = Camera.main;
            InitializeArea();
        }

        private void InitializeArea()
        {
            var screenRatio = (float)Screen.width / (float)Screen.height;

            Vector2 areaSize;
            
            areaSize.y = _camera.orthographicSize * 2;
            areaSize.x = areaSize.y * screenRatio;

            var position = areaSize / -2;

            _area = new Rect(position, areaSize);
        }
    }
}
