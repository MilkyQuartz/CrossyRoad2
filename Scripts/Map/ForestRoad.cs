using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestRoad : MonoBehaviour
{
    public List<Transform> treeObjectList = new List<Transform>();
    public int scaleNum = 1;
    public int startValue = - 12;
    public int endValue = 12;
    public int spawnRandom = 50;

    // Start is called before the first frame update
    void Start()
    {
        CreateTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTree()
    {
        int randomIndex = 0;
        int randomVal = 0;
        GameObject treeObj = null;

        Vector3 offsetPos = Vector3.zero;

        for (int i = startValue; i < endValue; i++)
        {
            randomVal = Random.Range(0, 100);
            if(randomVal < spawnRandom)
            {
                randomIndex = Random.Range(0, treeObjectList.Count);
                treeObj = GameObject.Instantiate(treeObjectList[randomIndex].gameObject);

                offsetPos.Set(i, 0.2f, 0f);

                treeObj.transform.SetParent(this.transform);
                treeObj.transform.localPosition = offsetPos;
            }

        }

    }
}
