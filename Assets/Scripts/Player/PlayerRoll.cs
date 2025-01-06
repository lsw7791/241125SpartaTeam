using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 0.1f; // 구를 거리
    public float rollSpeed = 10f; // 구르는 속도
    private Collider2D playerCollider; // 2D 플레이어의 콜라이더
    private Rigidbody2D rb; // 2D Rigidbody
    public bool isRolling = false; // 구르는 중인지 여부

    private Vector2 rollDirection; // 구르는 방향
    private Vector2 startPosition; // 구르기 시작 위치
    private float rollStartTime; // 구르기 시작 시간
    private float traveledDistance = 0f; // 이동한 거리

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>(); // 2D 콜라이더로 수정
        rb = GetComponent<Rigidbody2D>(); // 2D Rigidbody 가져오기
    }

    // 구르기 시작
    public void StartRolling()
    {
        if (isRolling) return; // 이미 구르고 있으면 시작하지 않음
        isRolling = true;

        // 방어력 증가
        GameManager.Instance.Player.IncreaseDefense(100);
        // 구르기 시작 위치는 이 스크립트가 붙은 오브젝트의 위치
        startPosition = transform.position;

        // 마우스 위치를 2D 월드 좌표로 변환
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 구를 방향은 플레이어에서 마우스로 향하는 방향
        rollDirection = (mousePosition - (Vector2)transform.position).normalized;

        // 구르기 시작 시간 기록
        rollStartTime = Time.time;

        // 구르기 시작 시 트리거를 비활성화 (충돌을 사용)
        playerCollider.isTrigger = false;
    }

    // 구르기 진행 (부드러운 이동)
    private void FixedUpdate()
    {
        if (!isRolling) return; // 구르기가 진행 중이지 않으면 실행하지 않음

        // 이동할 거리 계산 (구르기 시작 시간과 현재 시간을 기반으로 이동할 거리 계산)
        traveledDistance = (Time.time - rollStartTime) * rollSpeed;

        // 이동한 거리만큼 물리적 이동을 적용 (속도가 너무 빠르지 않게)
        Vector2 targetPosition = startPosition + rollDirection * rollDistance;
        rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, traveledDistance / rollDistance));

        // 구르기 종료 여부 체크
        if (traveledDistance >= rollDistance)
        {
            isRolling = false; // 구르기 종료
            GameManager.Instance.Player.ResetDefense();
        }
    }

    // "Wall" 레이어에 붙은 객체와 충돌 시 반대 방향으로 구르기
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // "Wall" 레이어에 충돌한 경우 구르기 방향 반전
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        rollDirection = -rollDirection;
    //        Debug.Log("Wall hit, changing direction!");
    //    }
    //}
}