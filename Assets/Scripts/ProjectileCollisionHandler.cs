using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private int _damage;

    // 데미지를 설정하는 초기화 메서드
    public void Initialize(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 객체가 플레이어인 경우는 무시
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // IDamageable을 구현하지 않아도, 충돌이 발생하면 파괴
        Destroy(gameObject);  // 발사체가 충돌하면 바로 사라짐

        // 충돌한 객체가 IDamageable을 구현한 경우 데미지를 적용
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);  // 데미지 처리
            Debug.Log($"Projectile dealt {_damage} damage to {damageable.GetType().Name}");
        }
    }
}
