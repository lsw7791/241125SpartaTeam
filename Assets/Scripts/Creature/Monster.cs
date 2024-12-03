using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    MonsterData monsterData;  // ���� ������

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData ������Ʈ ��������
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // ���Ͱ� �÷��̾�� �������� �� ��
            int damage = monsterData.creatureAttack;
            Debug.Log($"Monster dealt {damage} damage to {damageable.GetType().Name}");
            damageable.TakeDamage(damage);  // ������ ó��
        }
    }

    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void Die()
    {
        if (GameManager.Instance.monsterPool != null)
        {
            // ���͸� Ǯ�� ��ȯ (creatureId�� ����)
            GameManager.Instance.monsterPool.ReturnMonster(monsterData.id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        monsterData.isDie = true;  // ���� ���� ó��
        Debug.Log($"Monster {monsterData.creatureName} has died.");
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        Debug.Log($"Monster {monsterData.creatureName} takes {damage} damage.");
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            Debug.Log($"Monster {monsterData.creatureName} has been defeated.");
            Die();  // ü���� 0 ���ϰ� �Ǹ� ���� ó��
        }
        else
        {
            Debug.Log($"Monster {monsterData.creatureName} remaining health: {monsterData.currentHealth}");
        }
    }
}
