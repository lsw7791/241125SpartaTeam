using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ
    private Collider2D _weaponCollider;

    private void Awake()
    {
        _weapon = this.gameObject;
        EquipManager.Instance.WeaponObject = _weapon;
        _weaponCollider = _weapon.GetComponent<Collider2D>();  // ������ Collider2D ��������
        _weaponCollider.enabled = false;  // ���� �� �ݶ��̴� ��Ȱ��ȭ
    }


    // ���� �� ������ �ݶ��̴��� Ȱ��ȭ�ϴ� �޼���
    public void ActivateWeaponCollider()
    {
        _weaponCollider.enabled = true;  // ���� �� �ݶ��̴� Ȱ��ȭ
        Debug.Log("Weapon Collider Activated!");
    }

    // ���� �ð� �� ������ �ݶ��̴��� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    public IEnumerator DisableColliderAfterAttack()
    {
        yield return new WaitForSeconds(0.2f);  // ���� �� ��� ��ٸ�
        _weaponCollider.enabled = false;  // �ݶ��̴� ��Ȱ��ȭ
        Debug.Log("Weapon Collider Deactivated!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // �÷��̾ �浹�� ������Ʈ�� IDamageable�� ������ ���
            damageable.TakeDamage(_player.Stats.Damage);  // ������ ó��
            Debug.Log($"Player dealt {_player.Stats.Damage} damage to {damageable.GetType().Name}");
        }
    }
}
