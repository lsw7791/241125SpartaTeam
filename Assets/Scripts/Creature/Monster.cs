using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    MonsterData monsterData;  // 몬스터 데이터
    GameManager gameManager;   // 게임 매니저

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData 컴포넌트 가져오기
        gameManager = GameManager.Instance;  // 게임 매니저 인스턴스 가져오기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        damageable.TakeDamage(monsterData.creatureAttack);  // 데미지 처리
    }

    // 몬스터가 죽었을 때 호출되는 함수
    public void IsDie()
    {
        if (gameManager.monsterPool != null)
        {
            // 몬스터를 풀에 반환 (creatureId로 구별)
            gameManager.monsterPool.ReturnMonster(monsterData.creatureid, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        monsterData.isDie = true;  // 몬스터 죽음 처리
    }

    // 데미지를 입을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            IsDie();  // 체력이 0 이하가 되면 죽음 처리
        }
    }
}