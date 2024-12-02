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
        equippedWeapon = weapon; // ���� ���� ����
    }
}
