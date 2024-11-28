using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;   // 이동 속도
    private Vector2 moveInput; // 이동 입력값
    private Rigidbody2D rb;
    private Camera camera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // 이동 처리
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // 마우스 위치에 따른 회전 처리
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mouseScreenPos = context.ReadValue<Vector2>(); // 마우스 화면 좌표
            Vector2 mouseWorldPos = camera.ScreenToWorldPoint(mouseScreenPos); // 월드 좌표로 변환

            // 플레이어와 마우스 위치를 비교하여 좌우 반전
            FlipRotation(mouseWorldPos);
        }
    }

    private void FlipRotation(Vector2 mouseWorldPos)
    {
        // 마우스 위치가 플레이어의 위치보다 왼쪽에 있으면 Y축 회전 180도로 설정
        if (mouseWorldPos.x < transform.position.x)
        {
            // 왼쪽을 바라보게 (회전)
            transform.rotation = Quaternion.Euler(0, 180, 0);  // Y축 회전 180도
        }
        else
        {
            // 오른쪽을 바라보게 (회전 취소)
            transform.rotation = Quaternion.Euler(0, 0, 0);    // 기본 회전 (Y축 0도)
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed; // 이동 처리
    }
}
