using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReuseSystem
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
