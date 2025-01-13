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
        if (GameManager.Instance.Player.playerState == Player.PlayerState.Attack)
        {
            return;
        }

        GameManager.Instance.Player.playerState = Player.PlayerState.Attack;
        int atkType = GameManager.Instance.Player._playerWeapon.ATKType; // ATKType을 숫자로 가져옴

        if (GameManager.Instance.Player.stats.isDie) return; // 사망 상태라면 실행하지 않음
        if(!GameManager.Instance.Player.UseStamina(5)) return;// 스테미너 없으면 리턴

        switch (atkType)
        {
            case 2: // 화살 공격
                _animator.SetTrigger("Attack_Bow");
                SoundManager.Instance.PlayArrowSFX();
                GameManager.Instance.Player._playerWeapon.FireArrow(); // 화살 발사
                break;
            case 3: // 스태프 공격
                _animator.SetTrigger("Attack_Staff");
                SoundManager.Instance.PlayFireBallSFX();
                GameManager.Instance.Player._playerWeapon.FireFireball(); // 파이어볼 발사
                break;
            default: // 기본 근접 공격
                SoundManager.Instance.PlaySwordSFX();
                _animator.SetTrigger("Attack_Melee");
                break;
        }
    }
    public void SetTriggerAnimator(string name)
    {
        _animator.SetTrigger(name);
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

    public void OnAttackAnimationEnd()
    {
        GameManager.Instance.Player.playerState = Player.PlayerState.Idle;
    }
}
