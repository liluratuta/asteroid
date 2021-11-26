using System;
using UnityEngine;
using World;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                
                var go = new GameObject(typeof(T).ToString());
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<T>();
                _instance.Initialize();
                
                return _instance;
            }
        }
        
        private static T _instance;

        protected abstract void Initialize();
    }

