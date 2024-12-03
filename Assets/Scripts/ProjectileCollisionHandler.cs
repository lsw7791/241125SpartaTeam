using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private int _damage;

    // �������� �����ϴ� �ʱ�ȭ �޼���
    public void Initialize(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü�� �÷��̾��� ���� ����
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // IDamageable�� �������� �ʾƵ�, �浹�� �߻��ϸ� �ı�
        Destroy(gameObject);  // �߻�ü�� �浹�ϸ� �ٷ� �����

        // �浹�� ��ü�� IDamageable�� ������ ��� �������� ����
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);  // ������ ó��
            Debug.Log($"Projectile dealt {_damage} damage to {damageable.GetType().Name}");
        }
    }
}
