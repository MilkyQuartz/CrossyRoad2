using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    public WaterCar cloneWaterCar = null;
    public int scaleNum = 1;
    public Box cloneBox = null;
    public Transform createPos;
    public int createPersent = 50;
    public int createCarPersent = 10;
    public int createBoxPersent = 70;

    // ��� �ð����� ������ �� ������
    public float createTime = 3f;

    // �������� �ѹ��� ������ ������
    protected float createNextTime = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        float currentSec = Time.time;
        if(createNextTime <= currentSec)
        {
            int randomVal = Random.Range(0, 100);

            if(randomVal <= createPersent)
            {
                CloneObject();
            }
            createNextTime = currentSec + createTime;
        }
    }

    void CloneObject()
    {
        Transform clonePos = createPos;

        int randomVal = Random.Range(0, 100);

        bool createWaterCar = randomVal <= createCarPersent;
        bool createBox = !createWaterCar && randomVal <= createCarPersent + createBoxPersent;

        Vector3 offsetPos = clonePos.position;


        if (createWaterCar)
        {
            GameObject cloneObj = GameObject.Instantiate(cloneWaterCar.gameObject, offsetPos, Quaternion.Euler(0f, 90f, 0f), this.transform);
        }
        else if (createBox)
        {
            GameObject boxObj = GameObject.Instantiate(cloneBox.gameObject, offsetPos, Quaternion.Euler(0f, 90f, 0f), this.transform);
        }
    }

}
