using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 moveInput; // 이동 입력값
    private Rigidbody2D rb;
    [SerializeField] private Transform _playerObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
  
    private void FixedUpdate()
    {
        rb.velocity = moveInput * GameManager.Instance.Player.stats.MoveSpeed;  // 이동 처리
        if (GameManager.Instance.Player.playerState == Player.PlayerState.Attack)
        {
            return;
        }

        if (moveInput.x < 0)
        {
            // 왼쪽을 바라보게 (회전)
            _playerObject.rotation = Quaternion.Euler(0, 0, 0);  // Y축 회전 180도
        }
        else if (moveInput.x > 0)
        {
            // 오른쪽을 바라보게 (회전 취소)
            _playerObject.rotation = Quaternion.Euler(0, 180, 0);    // 기본 회전 (Y축 0도)
        }
    }

    public void FlipRotation(Vector2 mouseWorldPos)
    {
        // 마우스 위치가 플레이어의 위치보다 왼쪽에 있으면 Y축 회전 180도로 설정
        if (mouseWorldPos.x < transform.position.x)
        {
            // 왼쪽을 바라보게 (회전)
            _playerObject.rotation = Quaternion.Euler(0, 0, 0);  // Y축 회전 180도
        }
        else
        {
            // 오른쪽을 바라보게 (회전 취소)
            _playerObject.rotation = Quaternion.Euler(0, 180, 0);    // 기본 회전 (Y축 0도)
        }
    }
}
