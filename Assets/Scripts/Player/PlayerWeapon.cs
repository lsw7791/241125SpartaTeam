using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public int ATKType;
    [SerializeField] GameObject _weapon; // 현재 무기를 나타내는 오브젝트
    private Collider2D _weaponCollider;
    [SerializeField] GameObject[] _attackObjects; // 0번: 화살, 1번: 파이어볼
    [SerializeField] RectTransform _aoSpawnPoint; // 발사 위치

    [SerializeField]
    protected LayerMask layerMask; // 적군 레이어마스크

    private void Start()
    {
        //GameManager.Instance.Player._playerWeapon = this;
        _weapon = this.gameObject;
        GameManager.Instance.Player.Weapon = _weapon;
        _weaponCollider = _weapon.GetComponent<Collider2D>();  // 무기의 Collider2D 가져오기
        _weaponCollider.enabled = false;  // 시작 시 콜라이더 비활성화
    }
    public void TriggerAttack()
    {
        if (GameManager.Instance.Player.stats.isDie) return; // 사망 상태라면 실행하지 않음
        if (GameManager.Instance.Player.UseStamina(5)) return;// 스테미너 없으면 리턴

        switch (ATKType)
        {
            case 2: // 화살 공격
                GameManager.Instance.Player._playerAnimationController.SetTriggerAnimator("Attack_Bow");
                FireArrow(); // 화살 발사
                break;
            case 3: // 스태프 공격
                GameManager.Instance.Player._playerAnimationController.SetTriggerAnimator("Attack_Staff");
                FireFireball(); // 파이어볼 발사
                break;
            default: // 기본 근접 공격
                GameManager.Instance.Player._playerAnimationController.SetTriggerAnimator("Attack_Melee");
                break;
        }
    }
    // 공격 시 무기의 콜라이더를 활성화하는 메서드
    public void ActivateWeaponCollider()
    {
        _weaponCollider.enabled = true;  // 공격 시 콜라이더 활성화
    }

    // 무기 콜라이더 비활성화
    public void DeactivateWeaponCollider()
    {
        _weaponCollider.enabled = false; // 공격 종료 시 콜라이더 비활성화
    }


    // 화살 발사
    public void FireArrow()
    {
        //GameObject arrow = Instantiate(_attackObjects[0], _aoSpawnPoint.position, Quaternion.identity);
        GameObject arrow = GameManager.Instance.SpawnManager.projectilePool.SpawnFromPool("Arrow");
        if (arrow == null)
        {
            Debug.LogError("Arrow could not be spawned from pool.");
            return;
        }

        arrow.transform.position = _aoSpawnPoint.position;
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
        arrow.TryGetComponent<ProjectileCollisionHandler>(out var outProjectile);
        outProjectile.Initialize(layerMask, GameManager.Instance.Player.Stats.Damage+ GameManager.Instance.Player.Stats.PhysicalDamage);

        Debug.Log("Fired Arrow!");
    }


    // 파이어볼 발사
    public void FireFireball()
    {
        //GameObject fireball = Instantiate(_attackObjects[1], _aoSpawnPoint.position, Quaternion.identity);
        GameObject fireball = GameManager.Instance.SpawnManager.projectilePool.SpawnFromPool("FireBall");
        if (fireball == null)
        {
            Debug.LogError("Arrow could not be spawned from pool.");
            return;
        }

        fireball.transform.position = _aoSpawnPoint.position;
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
        fireball.TryGetComponent<ProjectileCollisionHandler>(out var outProjectile);
        outProjectile.Initialize(layerMask, GameManager.Instance.Player.Stats.Damage+ GameManager.Instance.Player.Stats.MagicalDamage);

        Debug.Log("Fired Fireball!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부모가 null인지 확인
        if (collision.transform.parent != null &&
            collision.transform.parent.TryGetComponent<ICreature>(out var outTarget))
        {
            // 플레이어가 충돌한 오브젝트가 ICreature를 구현한 경우
            outTarget.TakeDamage(GameManager.Instance.Player.Stats.Damage + GameManager.Instance.Player.Stats.PhysicalDamage);  // 데미지 처리
            Debug.Log($"Player dealt {GameManager.Instance.Player.Stats.Damage + GameManager.Instance.Player.Stats.PhysicalDamage} damage to {outTarget.GetType().Name}");
        }
        else
        {
            Debug.Log("Collision object does not have a parent or does not implement ICreature.");
        }
    }

}
