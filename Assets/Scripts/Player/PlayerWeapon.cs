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
        GameObject arrow = Instantiate(_attackObjects[0], _aoSpawnPoint.position, Quaternion.identity);
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        // 마우스 위치와 발사 위치 차이 계산
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = new Vector2(mousePosition.x - _aoSpawnPoint.position.x, mousePosition.y - _aoSpawnPoint.position.y);  // Vector2로 계산

        // 방향 벡터를 정규화하여 일정한 속도로 발사
        direction.Normalize();  // 벡터의 크기를 1로 만듬
        arrowRb.velocity = direction * 15f;  // 일정한 속도로 발사

        // 발사체가 마우스를 향하도록 회전 (화살이 자연스럽게 마우스를 향하도록)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // 각도 계산
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));  // 회전 적용

        // 충돌 처리 - 화살이 충돌 시 데미지 적용
        arrow.AddComponent<ProjectileCollisionHandler>().Initialize(_player.Stats.Damage);

        Debug.Log("Fired Arrow!");
    }


    // 파이어볼 발사
    public void FireFireball()
    {
        GameObject fireball = Instantiate(_attackObjects[1], _aoSpawnPoint.position, Quaternion.identity);
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();

        // 마우스 위치와 발사 위치 차이 계산
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = new Vector2(mousePosition.x - _aoSpawnPoint.position.x, mousePosition.y - _aoSpawnPoint.position.y);  // Vector2로 계산

        // 방향 벡터를 정규화하여 일정한 속도로 발사
        direction.Normalize();  // 벡터의 크기를 1로 만듬
        fireballRb.velocity = direction * 15f;  // 일정한 속도로 발사

        // 발사체가 마우스를 향하도록 회전 (파이어볼이 자연스럽게 마우스를 향하도록)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // 각도 계산
        fireball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));  // 회전 적용

        // 충돌 처리 - 파이어볼이 충돌 시 데미지 적용
        fireball.AddComponent<ProjectileCollisionHandler>().Initialize(_player.Stats.Damage);

        Debug.Log("Fired Fireball!");
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
