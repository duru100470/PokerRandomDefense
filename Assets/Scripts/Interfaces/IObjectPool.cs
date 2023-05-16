using System.Collections.Generic;
using UnityEngine;
public interface IObjectPool
{
    List<GameObject> ObjList { get; }
    GameObject Instantiate();
    GameObject Instantiate(Vector3 position);
    GameObject Instantiate(Vector3 position, Quaternion rotation);
    void Destroy(GameObject obj);
}
