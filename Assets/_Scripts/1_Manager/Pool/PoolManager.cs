using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public Pool[] pools;

    private void Awake()
    {
        Instance = this;

        foreach (Pool pool in pools)
        {
            pool.objects = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
                pool.objects.Add(CreateGameObject(pool.prefab));
        }
    }

    public GameObject Spawn(ObjectTag tag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                foreach (GameObject go in pool.objects)
                {
                    if (!go.activeInHierarchy)
                    {
                        go.SetActive(true);
                        return go;
                    }
                }

                // expand pool
                if (pool.expandable)
                {
                    GameObject go = CreateGameObject(pool.prefab);
                    pool.objects.Add(go);
                    go.SetActive(true);
                    return go;
                }
                else
                {
                    Logger.Warning("The pool with tag " + tag + " is not expandable, can't spawn more game object!");
                    return null;
                }
            }
        }

        Logger.Error("The pool with tag " + tag + " is not exist!");
        return null;
    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        return go;
    }
}