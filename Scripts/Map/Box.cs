using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float destroyDis = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
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
