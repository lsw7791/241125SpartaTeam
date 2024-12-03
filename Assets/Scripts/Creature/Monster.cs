using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    MonsterData monsterData;  // 몬스터 데이터

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData 컴포넌트 가져오기
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // 몬스터가 플레이어에게 데미지를 줄 때
            int damage = monsterData.creatureAttack;
            Debug.Log($"Monster dealt {damage} damage to {damageable.GetType().Name}");
            damageable.TakeDamage(damage);  // 데미지 처리
        }
    }

    // 몬스터가 죽었을 때 호출되는 함수
    public void Die()
    {
        if (GameManager.Instance.monsterPool != null)
        {
            // 몬스터를 풀에 반환 (creatureId로 구별)
            GameManager.Instance.monsterPool.ReturnMonster(monsterData.id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        monsterData.isDie = true;  // 몬스터 죽음 처리
        Debug.Log($"Monster {monsterData.creatureName} has died.");
    }

    // 데미지를 입을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        Debug.Log($"Monster {monsterData.creatureName} takes {damage} damage.");
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            Debug.Log($"Monster {monsterData.creatureName} has been defeated.");
            Die();  // 체력이 0 이하가 되면 죽음 처리
        }
        else
        {
            Debug.Log($"Monster {monsterData.creatureName} remaining health: {monsterData.currentHealth}");
        }
    }
}
