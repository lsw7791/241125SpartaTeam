using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
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
    public void ActivateWeaponCollider()
    {
        playerWeapon.ActivateWeaponCollider();
    }
    public void DeactivateWeaponCollider()
    {
        playerWeapon.DeactivateWeaponCollider();
    }
}
