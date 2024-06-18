using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoad : MonoBehaviour
{
    public Car[] cloneCar;
    public Transform[] createPos;
    public int createPersent = 70;
    public float createTime = 3f;
    protected float createNextTime = 0f;
    private CarPool carPool;
    public int scaleNum = 2;
    void Start()
    {
        carPool = CarPool.Instance;
    }

    void Update()
    {
        float currentSec = Time.time;
        if (createNextTime <= currentSec)
        {
            int randomVal = Random.Range(0, 100);

            if (randomVal <= createPersent)
            {
                CloneCar();
            }
            createNextTime = currentSec + createTime;
        }
    }

    void CloneCar()
    {
        int randPos = Random.Range(0, createPos.Length);
        Transform spawnPoint = createPos[randPos];
        int randCar = Random.Range(0, cloneCar.Length);

        GameObject carObj = carPool.GetPooledCar();
        if (carObj != null && !carObj.activeInHierarchy) 
        {
            carObj.transform.position = spawnPoint.position;

            if (randPos == 0)
            {
                carObj.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
            else if (randPos == 1)
            {
                carObj.transform.rotation = Quaternion.Euler(0f, -90f, 0f); 
            }

            carObj.SetActive(true);
        }
    }
}
