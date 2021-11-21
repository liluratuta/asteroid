using System;
using UnityEngine;

    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance => _instance;
        
        private static T _instance;

        protected virtual void Awake() => _instance = (T) this;
    }

