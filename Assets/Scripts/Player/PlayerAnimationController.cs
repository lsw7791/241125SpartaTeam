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
        int atkType = GameManager.Instance.Player._playerWeapon.ATKType; // ATKType을 숫자로 가져옴

        if (GameManager.Instance.Player.stats.isDie) return; // 사망 상태라면 실행하지 않음

        switch (atkType)
        {
            case 2: // 화살 공격
                _animator.SetTrigger("Attack_Bow");
                GameManager.Instance.Player._playerWeapon.FireArrow(); // 화살 발사
                break;
            case 3: // 스태프 공격
                _animator.SetTrigger("Attack_Staff");
                GameManager.Instance.Player._playerWeapon.FireFireball(); // 파이어볼 발사
                break;
            default: // 기본 근접 공격
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
        GameManager.Instance.Player._playerWeapon.ActivateWeaponCollider();
    }
    public void DeactivateWeaponCollider()
    {
        GameManager.Instance.Player._playerWeapon.DeactivateWeaponCollider();
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
