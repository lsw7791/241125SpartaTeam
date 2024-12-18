using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;   // 이동 속도
    private Vector2 moveInput; // 이동 입력값
    private Rigidbody2D rb;
    private Camera camera;
    private bool isDeath = false;

    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // 이동 처리
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isDeath) return;
        moveInput = context.ReadValue<Vector2>();
        bool isMoving = moveInput.sqrMagnitude > 0; // 벡터 크기로 이동 여부 판단
        GameManager.Instance.Player._playerAnimationController.SetMoveAnimation(isMoving);
    }

    // 마우스 위치에 따른 회전 처리
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(camera==null)
                camera = Camera.main;
            if(camera!=null)
            {
            Vector2 mouseScreenPos = context.ReadValue<Vector2>(); // 마우스 화면 좌표
            Vector2 mouseWorldPos = camera.ScreenToWorldPoint(mouseScreenPos); // 월드 좌표로 변환
            Vector2 direction =(Vector2)transform.position - mouseWorldPos;
            // 플레이어와 마우스 위치를 비교하여 좌우 반전
            GameManager.Instance.Player._playerAnimationController.FlipRotation(mouseWorldPos);
            RotateArm(direction);
            }
        }
    }
    public void TriggerDeath()
    {
        GameManager.Instance.Player.stats.isDie = true;
        speed = 0f;
        GameManager.Instance.Player._playerAnimationController.TriggerDeathAnimation(); // 죽음 애니메이션 실행
        this.enabled = false;

    }
    public void Revive()
    {
        UIManager.Instance.ToggleUI<DeathUI>();
        GameManager.Instance.Player.stats.isDie = false;
        speed = 3.5f;
        this.enabled = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed; // 이동 처리
    }
    public void RotateArm(Vector2 direction)
    {
        // 팔의 회전 계산
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);  // 회전 적용
    }
}
