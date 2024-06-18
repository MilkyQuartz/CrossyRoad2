using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CarSO carData;
    float moveSpeed;
    float destroyDis;

    private void Start()
    {
        moveSpeed = carData.moveSpeed;
        destroyDis = carData.destroyDis;
    }

    void Update()
    {
        MoveCar();
        CheckDestroy();
    }
    void MoveCar()
    {
        float moveZ = moveSpeed * Time.deltaTime;
        transform.Translate(0f, 0f, moveZ);
    }

    void CheckDestroy()
    {
        if (transform.localPosition.x <= -destroyDis)
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        gameObject.SetActive(false); 
    }
}
