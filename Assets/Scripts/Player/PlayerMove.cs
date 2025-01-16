using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 moveInput; // �̵� �Է°�
    private Rigidbody2D rb;
    [SerializeField] private Transform _playerObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
  
    private void FixedUpdate()
    {
        rb.velocity = moveInput * GameManager.Instance.Player.stats.MoveSpeed;  // �̵� ó��
        if (GameManager.Instance.Player.playerState == Player.PlayerState.Attack)
        {
            return;
        }

        if (moveInput.x < 0)
        {
            // ������ �ٶ󺸰� (ȸ��)
            _playerObject.rotation = Quaternion.Euler(0, 0, 0);  // Y�� ȸ�� 180��
        }
        else if (moveInput.x > 0)
        {
            // �������� �ٶ󺸰� (ȸ�� ���)
            _playerObject.rotation = Quaternion.Euler(0, 180, 0);    // �⺻ ȸ�� (Y�� 0��)
        }
    }

    public void FlipRotation(Vector2 mouseWorldPos)
    {
        // ���콺 ��ġ�� �÷��̾��� ��ġ���� ���ʿ� ������ Y�� ȸ�� 180���� ����
        if (mouseWorldPos.x < transform.position.x)
        {
            // ������ �ٶ󺸰� (ȸ��)
            _playerObject.rotation = Quaternion.Euler(0, 0, 0);  // Y�� ȸ�� 180��
        }
        else
        {
            // �������� �ٶ󺸰� (ȸ�� ���)
            _playerObject.rotation = Quaternion.Euler(0, 180, 0);    // �⺻ ȸ�� (Y�� 0��)
        }
    }
}
