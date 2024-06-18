using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCar : MonoBehaviour
{
    public CarSO carData;
    public float moveSpeed;
    public float destroyDis;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = carData.moveSpeed;
        destroyDis = carData.destroyDis;
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = moveSpeed * Time.deltaTime;
        this.transform.Translate(0f, 0f, moveZ);

        if (this.transform.localPosition.x >= destroyDis)
        {
            Destroy(gameObject);
        }
    }
}
