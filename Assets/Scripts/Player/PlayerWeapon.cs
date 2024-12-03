using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] private GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ

    private void Awake()
    {
        _weapon = this.gameObject;
        _weapon = EquipManager.Instance.WeaponObject;
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
