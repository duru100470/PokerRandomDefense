using System.Collections;
using System.Collections.Generic;
using PokerRandomDefense.DI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PokerRandomDefense.Infrastructure
{
    public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        LifetimeScope lifetimeScope;
        Queue<T> objQueue = new Queue<T>();
        [SerializeField]
        int amountToPool = 20;
        [SerializeField]
        GameObject objToPool;

        protected virtual void Awake()
        {
            lifetimeScope = LifetimeScope.Find<InGameScope>();
            Initialze(amountToPool);
        }

        private void Initialze(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                objQueue.Enqueue(CreateNewObject());
            }
        }

        protected virtual T CreateNewObject()
        {
            var obj = lifetimeScope.Container.Instantiate(objToPool, transform)
                .GetComponent<T>();
            obj.gameObject.SetActive(false);
            return obj;
        }

        public virtual T Instantiate()
        {
            if (objQueue.Count > 0)
            {
                var obj = objQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                return newObj;
            }
        }

        public virtual T Instantiate(Vector3 position)
        {
            if (objQueue.Count > 0)
            {
                var obj = objQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                obj.transform.position = position;
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                newObj.transform.position = position;
                return newObj;
            }
        }

        public virtual T Instantiate(Vector3 position, Quaternion rotation)
        {
            if (objQueue.Count > 0)
            {
                var obj = objQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(null);
                newObj.transform.position = position;
                newObj.transform.rotation = rotation;
                return newObj;
            }
        }

        public virtual void Destroy(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            objQueue.Enqueue(obj);
        }
    }
}