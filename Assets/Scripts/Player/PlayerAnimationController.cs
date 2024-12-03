using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private PlayerWeapon playerWeapon; // PlayerWeapon 스크립트가 붙은 오브젝트 참조


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
                playerWeapon.FireArrow();  // 화살 발사
                break;
            case "Staff":
                _animator.SetTrigger("Attack_Staff");
                playerWeapon.FireFireball();  // 파이어볼 발사
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
