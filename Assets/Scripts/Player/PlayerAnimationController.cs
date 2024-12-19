using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator _animator;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {  
        GameManager.Instance.Player._playerAnimationController = this;
    }
    public void SetMoveAnimation(bool isMoving)
    {
        _animator.SetBool("Move", isMoving);
    }
    public void TriggerAttackAnimation()
    {
        int atkType = GameManager.Instance.Player._playerWeapon.ATKType; // ATKType�� ���ڷ� ������

        if (GameManager.Instance.Player.stats.isDie) return; // ��� ���¶�� �������� ����

        switch (atkType)
        {
            case 2: // ȭ�� ����
                _animator.SetTrigger("Attack_Bow");
                GameManager.Instance.Player._playerWeapon.FireArrow(); // ȭ�� �߻�
                break;
            case 3: // ������ ����
                _animator.SetTrigger("Attack_Staff");
                GameManager.Instance.Player._playerWeapon.FireFireball(); // ���̾ �߻�
                break;
            default: // �⺻ ���� ����
                _animator.SetTrigger("Attack_Melee");
                break;
        }
    }

    public void SetPaddingAnimation(bool isPadding)
    {
        _animator.SetBool("Padding", isPadding);
    }
    public void TriggerDeathAnimation()
    {
        _animator.SetBool("isDeath", true); // ���� ���� Ȱ��ȭ
        _animator.SetTrigger("Death");     // ���� Ʈ���� ����
    }

    public void ActivateWeaponCollider()
    {
        GameManager.Instance.Player._playerWeapon.ActivateWeaponCollider();
    }
    public void DeactivateWeaponCollider()
    {
        GameManager.Instance.Player._playerWeapon.DeactivateWeaponCollider();
    }
    public void FlipRotation(Vector2 mouseWorldPos)
    {
        // ���콺 ��ġ�� �÷��̾��� ��ġ���� ���ʿ� ������ Y�� ȸ�� 180���� ����
        if (mouseWorldPos.x < transform.position.x)
        {
            // ������ �ٶ󺸰� (ȸ��)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Y�� ȸ�� 180��
        }
        else
        {
            // �������� �ٶ󺸰� (ȸ�� ���)
            transform.rotation = Quaternion.Euler(0, 180, 0);    // �⺻ ȸ�� (Y�� 0��)
        }
    }
}
