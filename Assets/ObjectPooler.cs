using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ObjectPooler : MonoBehaviour 
{

    [System.Serializable]
    public struct ObjectPoolItem
    {
        public int amountToPull;
        public GameObject objectToPool;
        public bool shouldExpand;
    }

    [SerializeField]  public List<ObjectPoolItem> pooledObjects;

    List<GameObject> objectPool;

    void Awake()
    {
        objectPool = new List<GameObject>();
        foreach (ObjectPoolItem item in pooledObjects)
        {
            for (int i = 0; i < item.amountToPull; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }
    }

    void Start()
    {

    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy && objectPool[i].tag == tag)
            {
                return objectPool[i];
            }
        }
        foreach (ObjectPoolItem item in pooledObjects)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    objectPool.Add(obj);
                    return obj;
                }
            }
        }
        Debug.Log("not found" + objectPool.Count);
        return null;
    }
}
