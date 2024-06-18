using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(4.0f, 8.0f, -5.0f);
    public float smoothSpeed = 0f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
