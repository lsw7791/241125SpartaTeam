using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Monster : MonoBehaviour, ICreature
{
    [SerializeField]private int currentHealth;
    [SerializeField]private bool isDie;
    [SerializeField] public int id { get; private set; }
    // 몬스터가 죽었을 때 호출되는 함수
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// 스텟 초기화
    }
    public void Die()
    {
        if (SpawnManager.Instance.monsterPool != null)
        {
            // 몬스터의 종류를 구분해서 풀에 반환 (creatureId로 구별)
            SpawnManager.Instance.monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        isDie = true;  // 몬스터 죽음 처리
        DropItems();  // 아이템 드랍 함수 호출
    }

    // 데미지를 입을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();  // 체력이 0 이하가 되면 죽음 처리
        }
        else
        {
            Debug.Log($"Monster {GameManager.Instance.dataManager.creature.GetName(id)} remaining health: {currentHealth}");
        }
    }

    // 몬스터가 죽을 때 호출되는 드랍 아이템 생성 함수
    public void DropItems()
    {
        // 드롭 위치의 랜덤 범위 설정 (x, z 축 기준)
        float dropRange = 1.5f;

        foreach (int itemId in GameManager.Instance.dataManager.creature.GetDropItemIds(id))
        {
            // 아이템 데이터와 프리팹 로드
            var itemData = GameManager.Instance.dataManager.GetItemDataById(itemId);
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);

            // 랜덤한 드롭 위치 생성
            Vector3 randomOffset = new Vector3(
                Random.Range(-dropRange, dropRange),
                Random.Range(-dropRange, dropRange),
                transform.position.z
            );
            Vector3 dropPosition = transform.position + randomOffset;

            // 아이템 인스턴스 생성
            GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

            // 아이템 스프라이트 로드
            Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);

            // 아이템 데이터 설정
            TestItem testItem = item.GetComponent<TestItem>();
            testItem.SetData(itemData, itemSprite);
        }
    }


    public void ResetStatus()
    {
        currentHealth = GameManager.Instance.dataManager.creature.GetHealth(id);  // 최대 체력으로 리셋
        isDie = false;  // 죽지 않은 상태로 리셋
    }
}
