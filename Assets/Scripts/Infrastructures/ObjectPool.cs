using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokerRandomDefense.DI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PokerRandomDefense.Infrastructure
{
    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        private LifetimeScope lifetimeScope;
        private Queue<GameObject> objQueue = new Queue<GameObject>();
        private List<GameObject> objsInHierarchy = new List<GameObject>();
        [SerializeField]
        private GameObject objToPool;

        public List<GameObject> ObjList
            => objsInHierarchy.ToList();

        public void Initialze(int amount, LifetimeScope lifetimeScope, GameObject obj)
        {
            this.lifetimeScope = lifetimeScope;
            objToPool = obj;

            for (int i = 0; i < amount; i++)
            {
                objQueue.Enqueue(CreateNewObject());
            }
        }

        protected virtual GameObject CreateNewObject()
        {
            var obj = lifetimeScope.Container.Instantiate(objToPool, transform);
            obj.SetActive(false);
            return obj;
        }

        public virtual GameObject Instantiate()
        {
            if (objQueue.Count > 0)
            {
                var obj = objQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.SetActive(true);
                objsInHierarchy.Add(obj);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.SetActive(true);
                newObj.transform.SetParent(null);
                objsInHierarchy.Add(newObj);
                return newObj;
            }
        }

        public virtual GameObject Instantiate(Vector3 position)
        {
            if (objQueue.Count > 0)
            {
                var obj = objQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.SetActive(true);
                obj.transform.position = position;
                objsInHierarchy.Add(obj);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.SetActive(true);
                newObj.transform.SetParent(null);
                newObj.transform.position = position;
                objsInHierarchy.Add(newObj);
                return newObj;
            }
        }

        public virtual GameObject Instantiate(Vector3 position, Quaternion rotation)
        {
            if (objQueue.Count > 0)
            {
                var obj = objQueue.Dequeue();
                obj.transform.SetParent(null);
                obj.SetActive(true);
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                objsInHierarchy.Add(obj);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.SetActive(true);
                newObj.transform.SetParent(null);
                newObj.transform.position = position;
                newObj.transform.rotation = rotation;
                objsInHierarchy.Add(newObj);
                return newObj;
            }
        }

        public virtual void Destroy(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            objsInHierarchy.Remove(obj);
            objQueue.Enqueue(obj);
        }
    }
}