using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public ObjectPool objectPool;
    public ApplePool applePool;
    public Transform parentTransform;

    public int maxPosZ = 10;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float playerZPosition = 0f;
    Vector3 currentPosition = Vector3.zero;

    void Start()
    {
        objectPool.Initialize(10);
        applePool.Initialize(5); 

        while (currentPosition.z <= maxPosZ)
        {
            GameObject roadObj = objectPool.GetObject();
            float objLength = GetObjectLength(roadObj);

            roadObj.transform.SetParent(parentTransform);
            roadObj.transform.localPosition = currentPosition;

            spawnedObjects.Add(roadObj);
            currentPosition.z += objLength;

            GameObject appleObj = applePool.GetObject(); 
            appleObj.transform.position = GetRandomPosition();  
        }
    }

    float GetObjectLength(GameObject obj)
    {
        float length = 0f;

        ForestRoad forestRoad = obj.GetComponent<ForestRoad>();
        River river = obj.GetComponent<River>();
        CarRoad carRoad = obj.GetComponent<CarRoad>();
        Road road = obj.GetComponent<Road>();

        if (forestRoad != null)
        {
            length = forestRoad.scaleNum;
        }
        else if (river != null)
        {
            length = river.scaleNum;
        }
        else if (carRoad != null)
        {
            length = carRoad.scaleNum;
        }
        else if (road != null)
        {
            length = road.scaleNum;
        }
        else
        {
            Debug.LogWarning("이 오브젝트에 scaleNum 변수명을 가져올 수 없음: " + obj.name);
        }

        return length;
    }

    public void MoveMap(float playerZ)
    {
        playerZPosition = playerZ;

        if (spawnedObjects.Count > 0 && playerZPosition > spawnedObjects[0].transform.localPosition.z + GetObjectLength(spawnedObjects[0]))
        {
            GameObject firstObject = spawnedObjects[0];
            spawnedObjects.RemoveAt(0);

            objectPool.ReturnObject(firstObject);

            GameObject newRoadObj = objectPool.GetObject();
            float objLength = GetObjectLength(newRoadObj);

            newRoadObj.transform.SetParent(parentTransform);

            newRoadObj.transform.localPosition = currentPosition;

            currentPosition.z += objLength;
            spawnedObjects.Add(newRoadObj);

            GameObject appleObj = applePool.GetObject(); 
            appleObj.transform.position = GetRandomPosition(); 
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(0f, 8f);
        return new Vector3(randomX, 1f, randomZ);
    }
}
