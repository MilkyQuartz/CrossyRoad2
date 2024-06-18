using System.Collections.Generic;
using UnityEngine;

public class CarPool : MonoBehaviour
{
    public static CarPool Instance;

    public GameObject[] carPrefabs;
    public int poolSize = 20;
    private List<GameObject> carPool;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        carPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject car = InstantiateRandomCar();
            car.SetActive(false);
            carPool.Add(car);
        }
    }

    public GameObject GetPooledCar()
    {
        foreach (GameObject car in carPool)
        {
            if (!car.activeInHierarchy)
            {
                return car;
            }
        }

        GameObject newCar = InstantiateRandomCar();
        newCar.SetActive(false);
        carPool.Add(newCar);
        return newCar;
    }

    private GameObject InstantiateRandomCar()
    {
        GameObject randomPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        return Instantiate(randomPrefab, Vector3.zero, Quaternion.identity, transform);
    }
}
