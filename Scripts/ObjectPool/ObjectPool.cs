using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] prefabs; 
    private Queue<GameObject> pool = new Queue<GameObject>();

    public void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(1, prefabs.Length);
            GameObject obj = null;
            if(i < 1) // 처음 시작 그라운드
            {
                obj = Instantiate(prefabs[0]);
            }
            else
            {
                obj = Instantiate(prefabs[randomIndex]);
            }
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
            int randomIndex = Random.Range(1, prefabs.Length);
            GameObject obj = Instantiate(prefabs[randomIndex]);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
