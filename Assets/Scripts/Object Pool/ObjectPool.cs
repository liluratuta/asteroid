using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.ObjectPools
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private int _capacity = 30;

        private Stack<T> _freeObjects;
        private readonly HashSet<T> _unfreeObjects = new HashSet<T>();

        public T Get()
        {
            var poolObject = _freeObjects.Pop();
            _unfreeObjects.Add(poolObject);

            poolObject.gameObject.SetActive(true);
            
            return poolObject;
        }

        public void Reclaim(T poolObject)
        {
            if (!_unfreeObjects.Contains(poolObject))
            {
                return;
            }

            poolObject.gameObject.SetActive(false);
            
            _unfreeObjects.Remove(poolObject);
            _freeObjects.Push(poolObject);
        }

        protected abstract List<T> MakePoolObjects(int capacity);

        protected virtual void Awake()
        {
            List<T> poolObjects = MakePoolObjects(_capacity);

            foreach (var poolObject in poolObjects)
            {
                poolObject.gameObject.SetActive(false);
            }
            
            _freeObjects = new Stack<T>(poolObjects);
        }
    }
}