using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    float cameraX = 4.0f;
    float cameraY = 8.0f;
    float cameraZ = -5.0f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 playerPos = new Vector3(player.position.x + cameraX, player.position.y + cameraY, cameraZ);
            transform.position = playerPos;
        }
    }
}
