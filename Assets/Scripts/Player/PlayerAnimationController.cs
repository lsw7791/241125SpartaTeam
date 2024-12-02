using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject equippedWeapon; // 현재 장착된 무기


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
        // 장착된 무기가 없거나 태그가 설정되지 않은 경우 기본값으로 "Attack_Melee" 사용
        string weaponTag = equippedWeapon?.tag ?? "Melee";

        switch (weaponTag)
        {
            case "Bow":
                _animator.SetTrigger("Attack_Bow");
                break;
            case "Staff":
                _animator.SetTrigger("Attack_Staff");
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
    public void SetEquippedWeapon(GameObject weapon)
    {
        equippedWeapon = weapon; // 장착 무기 설정
    }
}
