using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;   // 이동 속도
    private Vector2 moveInput; // 이동 입력값
    private Rigidbody2D rb;
    private Camera camera;
    private UnitRoot unitRoot;
    private TopDownAimRotation topDownAimRotation;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        unitRoot = GetComponentInChildren<UnitRoot>();
        topDownAimRotation =GetComponent<TopDownAimRotation>();
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
            Vector2 direction =(Vector2)transform.position - mouseWorldPos;
            // 플레이어와 마우스 위치를 비교하여 좌우 반전
            unitRoot.FlipRotation(mouseWorldPos);
            topDownAimRotation.RotateArm(direction);
        }
    }

    

    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed; // 이동 처리
    }
}
