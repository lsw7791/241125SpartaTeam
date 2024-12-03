using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject equippedWeapon; // ���� ������ ����


    void Start()
    {
        _animator = GetComponent<Animator>();
        equippedWeapon = null;
    }

    public void SetMoveAnimation(bool isMoving)
    {
        _animator.SetBool("Move", isMoving);
    }
    public void TriggerAttackAnimation()
    {
        // ������ ���Ⱑ ���ų� �±װ� �������� ���� ��� �⺻������ "Attack_Melee" ���
        PlayerWeapon playerWeapon = EquipManager.Instance.WeaponObject.GetComponent<PlayerWeapon>();

        string weaponTag = EquipManager.Instance.EquipedWeapon?.tag ?? "Melee";

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
                playerWeapon.ActivateWeaponCollider();  // ���� �ݶ��̴� Ȱ��ȭ
                break;
        }
    }
    public void SetPaddingAnimation(bool isPadding)
    {
        _animator.SetBool("Padding", isPadding);
    }
    public void SetEquippedWeapon(GameObject weapon)
    {
        equippedWeapon = weapon; // ���� ���� ����
    }
}
