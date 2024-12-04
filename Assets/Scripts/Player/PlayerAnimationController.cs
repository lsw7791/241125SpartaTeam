using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public bool isDeath { get; set; } // 죽음 상태 여부

    private Animator _animator;




    private void Awake()
    {
        _animator = GetComponent<Animator>();   
        Player.Instance._playerAnimationController = this;
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
                Player.Instance._playerWeapon.FireArrow();  // 화살 발사
                break;
            case "Staff":
                _animator.SetTrigger("Attack_Staff");
                Player.Instance._playerWeapon.FireFireball();  // 파이어볼 발사
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
        _animator.SetBool("isDeath", true); // 죽음 상태 활성화
        _animator.SetTrigger("Death");     // 죽음 트리거 실행
    }

    public void ActivateWeaponCollider()
    {
        Player.Instance._playerWeapon.ActivateWeaponCollider();
    }
    public void DeactivateWeaponCollider()
    {
        Player.Instance._playerWeapon.DeactivateWeaponCollider();
    }
    public void FlipRotation(Vector2 mouseWorldPos)
    {
        // 마우스 위치가 플레이어의 위치보다 왼쪽에 있으면 Y축 회전 180도로 설정
        if (mouseWorldPos.x < transform.position.x)
        {
            // 왼쪽을 바라보게 (회전)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Y축 회전 180도
        }
        else
        {
            // 오른쪽을 바라보게 (회전 취소)
            transform.rotation = Quaternion.Euler(0, 180, 0);    // 기본 회전 (Y축 0도)
        }
    }
}
