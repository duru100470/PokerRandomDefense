using System;
using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.DI;
using UnityEngine;
using VContainer.Unity;

namespace PokerRandomDefense.Infrastructure
{
    public class ObjectPoolingSystem : MonoBehaviour
    {
        private LifetimeScope lifetimeScope;
        private Dictionary<ObjectList, IObjectPool> pools = new Dictionary<ObjectList, IObjectPool>();
        [SerializeField]
        private List<Registration> registrations = new List<Registration>();

        public IObjectPool this[ObjectList key]
        {
            get { return pools[key]; }
        }

        protected virtual void Awake()
        {
            lifetimeScope = LifetimeScope.Find<InGameScope>();

            registrations.ForEach(r => 
            {
                RegisterObject(r.obj, r.key);
            });
        }

        public void RegisterObject(GameObject obj, ObjectList key, int amount = 20)
        {
            if (pools.ContainsKey(key)) return;

            var poolObj = new GameObject(key.ToString() + "_Pool");
            poolObj.transform.SetParent(transform);
            var pool = poolObj.AddComponent<ObjectPool>();
            pool.Initialze(amount, lifetimeScope, obj);

            pools.Add(key, pool);
        }

        public void RemoveObject(ObjectList key)
            => pools.Remove(key);

        [Serializable]
        public struct Registration
        {
            public ObjectList key;
            public GameObject obj;
        }
    }

    public enum ObjectList
    {
        Enemy,
        Projectile
    }
}