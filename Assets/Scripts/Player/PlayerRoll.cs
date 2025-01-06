using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 0.1f; // ���� �Ÿ�
    public float rollSpeed = 10f; // ������ �ӵ�
    private Collider2D playerCollider; // 2D �÷��̾��� �ݶ��̴�
    private Rigidbody2D rb; // 2D Rigidbody
    public bool isRolling = false; // ������ ������ ����

    private Vector2 rollDirection; // ������ ����
    private Vector2 startPosition; // ������ ���� ��ġ
    private float rollStartTime; // ������ ���� �ð�
    private float traveledDistance = 0f; // �̵��� �Ÿ�

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>(); // 2D �ݶ��̴��� ����
        rb = GetComponent<Rigidbody2D>(); // 2D Rigidbody ��������
    }

    // ������ ����
    public void StartRolling()
    {
        if (isRolling) return; // �̹� ������ ������ �������� ����
        isRolling = true;

        // ���� ����
        GameManager.Instance.Player.IncreaseDefense(100);
        // ������ ���� ��ġ�� �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ
        startPosition = transform.position;

        // ���콺 ��ġ�� 2D ���� ��ǥ�� ��ȯ
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���� ������ �÷��̾�� ���콺�� ���ϴ� ����
        rollDirection = (mousePosition - (Vector2)transform.position).normalized;

        // ������ ���� �ð� ���
        rollStartTime = Time.time;

        // ������ ���� �� Ʈ���Ÿ� ��Ȱ��ȭ (�浹�� ���)
        playerCollider.isTrigger = false;
    }

    // ������ ���� (�ε巯�� �̵�)
    private void FixedUpdate()
    {
        if (!isRolling) return; // �����Ⱑ ���� ������ ������ �������� ����

        // �̵��� �Ÿ� ��� (������ ���� �ð��� ���� �ð��� ������� �̵��� �Ÿ� ���)
        traveledDistance = (Time.time - rollStartTime) * rollSpeed;

        // �̵��� �Ÿ���ŭ ������ �̵��� ���� (�ӵ��� �ʹ� ������ �ʰ�)
        Vector2 targetPosition = startPosition + rollDirection * rollDistance;
        rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, traveledDistance / rollDistance));

        // ������ ���� ���� üũ
        if (traveledDistance >= rollDistance)
        {
            isRolling = false; // ������ ����
            GameManager.Instance.Player.ResetDefense();
        }
    }

    // "Wall" ���̾ ���� ��ü�� �浹 �� �ݴ� �������� ������
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // "Wall" ���̾ �浹�� ��� ������ ���� ����
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        rollDirection = -rollDirection;
    //        Debug.Log("Wall hit, changing direction!");
    //    }
    //}
}