using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Pool
{
    public GameObject prefab;
    public string tag;
    public int count;
    public bool expandable;
}

public class ObjectPooler : Singleton<ObjectPooler>
{
    [SerializeField] private List<Pool> pools; // add in Inspector
    private Dictionary<string, List<GameObject>> poolDictionary;

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);

        this.poolDictionary = new Dictionary<string, List<GameObject>>();
        foreach (Pool pool in pools)
        {
            List<GameObject> objects = new List<GameObject>();
            for (int i = 0; i < pool.count; i++)
            {
                objects.Add(CreateGameObject(pool.prefab));
            }
            this.poolDictionary.Add(pool.tag, objects);
        }
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                return obj;
            }
        }

        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                if (pool.expandable)
                {
                    // expand pool
                    GameObject obj = CreateGameObject(pool.prefab);
                    poolDictionary[tag].Add(obj);
                    obj.SetActive(true);
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    return obj;
                }
                break;
            }
        }

        return null;
    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        return obj;
    }

    //public void FreePool(string tag)
    //{
    //    if (!poolDictionary.ContainsKey(tag))
    //    {
    //        Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
    //        return;
    //    }

    //    foreach (GameObject obj in poolDictionary[tag])
    //        Destroy(obj);

    //    poolDictionary[tag].Clear();
    //}
}