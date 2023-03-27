using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    LifetimeScope lifeTimeScope;
    Queue<T> objQueue = new Queue<T>();
    [SerializeField]
    int amountToPool = 20;
    [SerializeField]
    GameObject objToPool;

    protected virtual void Awake()
    {
        lifeTimeScope = GameObject.FindObjectOfType<InGameLifeTimeScope>();
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
        var obj = lifeTimeScope.Container.Instantiate(objToPool)
            .GetComponent<T>();
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    public virtual T Instantiate()
    {
        if(objQueue.Count > 0)
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

    public virtual void Destroy(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        objQueue.Enqueue(obj);
    }
}
