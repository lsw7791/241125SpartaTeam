using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] private GameObject _weapon; // 현재 무기를 나타내는 오브젝트

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
            // 플레이어가 충돌한 오브젝트가 IDamageable을 구현한 경우
            damageable.TakeDamage(_player.Stats.Damage);  // 데미지 처리
            Debug.Log($"Player dealt {_player.Stats.Damage} damage to {damageable.GetType().Name}");
        }
    }


}
