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
        int atkType = GameManager.Instance.Player._playerWeapon.ATKType; // ATKType�� ���ڷ� ������

        if (GameManager.Instance.Player.stats.isDie) return; // ��� ���¶�� �������� ����
        if(!GameManager.Instance.Player.UseStamina(5)) return;// ���׹̳� ������ ����

        switch (atkType)
        {
            case 2: // ȭ�� ����
                _animator.SetTrigger("Attack_Bow");
                SoundManager.Instance.PlayArrowSFX();
                GameManager.Instance.Player._playerWeapon.FireArrow(); // ȭ�� �߻�
                break;
            case 3: // ������ ����
                _animator.SetTrigger("Attack_Staff");
                SoundManager.Instance.PlayFireBallSFX();
                GameManager.Instance.Player._playerWeapon.FireFireball(); // ���̾ �߻�
                break;
            default: // �⺻ ���� ����
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

    public void OnAttackAnimationEnd()
    {
        GameManager.Instance.Player.playerState = Player.PlayerState.Idle;
    }
}
