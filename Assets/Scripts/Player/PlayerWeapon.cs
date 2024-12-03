using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _weapon; // 현재 무기를 나타내는 오브젝트
    private Collider2D _weaponCollider;
    [SerializeField] GameObject[] _attackObjects; // 0번: 화살, 1번: 파이어볼
    [SerializeField] RectTransform _aoSpawnPoint; // 발사 위치

    private void Awake()
    {
        _weapon = this.gameObject;
        EquipManager.Instance.WeaponObject = _weapon;
        _weaponCollider = _weapon.GetComponent<Collider2D>();  // 무기의 Collider2D 가져오기
        _weaponCollider.enabled = false;  // 시작 시 콜라이더 비활성화
    }

    // 공격 시 무기의 콜라이더를 활성화하는 메서드
    public void ActivateWeaponCollider()
    {
        _weaponCollider.enabled = true;  // 공격 시 콜라이더 활성화
        Debug.Log("Weapon Collider Activated!");
    }

    // 일정 시간 후 무기의 콜라이더를 비활성화하는 코루틴
    public IEnumerator DisableColliderAfterAttack()
    {
        yield return new WaitForSeconds(0.2f);  // 공격 후 잠시 기다림
        _weaponCollider.enabled = false;  // 콜라이더 비활성화
        Debug.Log("Weapon Collider Deactivated!");
    }

    // 화살 발사
    public void FireArrow()
    {
        if (_attackObjects.Length > 0 && _attackObjects[0] != null)
        {
            GameObject arrow = Instantiate(_attackObjects[0], _aoSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _aoSpawnPoint.position).normalized;
            arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f;  // 속도는 적절히 설정
            Debug.Log("Fired Arrow!");
        }
    }

    // 파이어볼 발사
    public void FireFireball()
    {
        if (_attackObjects.Length > 1 && _attackObjects[1] != null)
        {
            GameObject fireball = Instantiate(_attackObjects[1], _aoSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _aoSpawnPoint.position).normalized;
            fireball.GetComponent<Rigidbody2D>().velocity = direction * 5f;  // 속도는 적절히 설정
            Debug.Log("Fired Fireball!");
        }
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
