using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isMoving = false;
    private Vector3 boxOffsetPosition = Vector3.zero;
    private GameObject boxObject;
    public float fallMultiplier = 2.0f;
    private MapManager mapManager;

    public float jumpForce = 0.3f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(0, 1, -3.35f);
        mapManager = FindObjectOfType<MapManager>();
    }

    void Update()
    {
        if (moveInput != Vector2.zero && !isMoving)
        {
            StartCoroutine(Move());

        }
        if (boxObject != null)
        {
            Vector3 playerPos = boxObject.transform.position + boxOffsetPosition;
            rb.MovePosition(playerPos);
        }
        // 플레이어의 현재 위치를 기반으로 맵을 갱신
        mapManager.MoveMap(transform.position.z);

        // 내려올때 중력 쎄게 주기
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    private IEnumerator Move()
    {
        isMoving = true;
        Vector3 startPosition = rb.position;
        Vector3 targetPosition = rb.position + new Vector3(moveInput.x, 0.35f, moveInput.y);

        float duration = 0.1f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPosition);
        isMoving = false;
        moveInput = Vector2.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            boxObject = collision.gameObject;
            boxOffsetPosition = transform.position - boxObject.transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            boxObject = null;
            boxOffsetPosition = Vector3.zero;
        }
    }
}
