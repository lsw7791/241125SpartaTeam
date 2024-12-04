using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public bool isDeath { get; set; } // ���� ���� ����

    private Animator _animator;
    [SerializeField] private PlayerWeapon playerWeapon; // PlayerWeapon ��ũ��Ʈ�� ���� ������Ʈ ����


    void Start()
    {
        _animator = GetComponent<Animator>();
        playerWeapon = EquipManager.Instance.WeaponObject.GetComponent<PlayerWeapon>();
    }

    public void SetMoveAnimation(bool isMoving)
    {
        _animator.SetBool("Move", isMoving);
    }
    public void TriggerAttackAnimation()
    {

        string weaponTag = EquipManager.Instance.EquipedWeapon?.tag ?? "Melee";

        if (isDeath) return;

        switch (weaponTag)
        {
            case "Bow":
                _animator.SetTrigger("Attack_Bow");
                playerWeapon.FireArrow();  // ȭ�� �߻�
                break;
            case "Staff":
                _animator.SetTrigger("Attack_Staff");
                playerWeapon.FireFireball();  // ���̾ �߻�
                break;
            default:
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
        playerWeapon.ActivateWeaponCollider();
    }
    public void DeactivateWeaponCollider()
    {
        playerWeapon.DeactivateWeaponCollider();
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
