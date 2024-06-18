using System.Collections.Generic;
using UnityEngine;

public class ApplePool : MonoBehaviour
{
    public GameObject applePrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Initialize(poolSize);
    }

    public void Initialize(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(applePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(applePrefab);
            newObj.SetActive(true);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
