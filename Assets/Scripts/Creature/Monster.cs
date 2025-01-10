using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour, ICreature
{
    public int currentHealth { get; private set; }
   // private bool _isDie;
    [SerializeField] public int id { get; private set; }
    MonsterPool monsterPool;
    public Vector2 DiePosition;
    MonsterHealthBarUI monsterHealthBarUI;
    private DamageUIController damageUIController;
    // 몬스터가 죽었을 때 호출되는 함수
    private void Start()
    {
        monsterPool = GetComponentInParent<MonsterPool>();
        monsterHealthBarUI = GetComponent<MonsterHealthBarUI>();
        damageUIController = GetComponent<DamageUIController>();
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// 스텟 초기화
    }
    public void Die()
    {
        DiePosition = gameObject.transform.position;

        if (monsterPool != null)
        {
            // 몬스터의 종류를 구분해서 풀에 반환 (creatureId로 구별)
            monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

       // _isDie = true;  // 몬스터 죽음 처리
        DropItems();  // 아이템 드랍 함수 호출
    }

    // 데미지를 입을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        int value = GameManager.Instance.DataManager.Creature.GetDefense(id);
        value -= damage;
        if (value < 0)
        {
            currentHealth += value;
            //이미지 체력 업데이트
            monsterHealthBarUI.UpdateHealthBar();
            Vector3 position = transform.position + Vector3.up; // 머리 위
            damageUIController.ShowDamage(position, damage);
            if (currentHealth <= 0)
            {
                Die();  // 체력이 0 이하가 되면 죽음 처리
            }    
        }     
    }

    // 몬스터가 죽을 때 호출되는 드랍 아이템 생성 함수
    public void DropItems()
    {
        float dropRange = 0.5f; // 드롭 위치의 랜덤 범위 설정

        foreach (int itemId in GameManager.Instance.DataManager.Creature.GetDropItemIds(id))
        {
            var itemData = GameManager.Instance.DataManager.GetItemDataById(itemId);

            // 아이템 데이터와 프리팹 로드
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);

            Vector2 randomOffset = new Vector3(
                Random.Range(-dropRange, dropRange),
                Random.Range(-dropRange, dropRange)
            );
            Vector2 dropPosition = DiePosition + randomOffset ;

            GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

            TestItem testItem = item.GetComponent<TestItem>();

            if (itemData.itemType == ItemType.Gold && itemData.goldRange != null && itemData.goldRange.Count == 2)
            {
                int minGold = itemData.goldRange[0]; // 최소 골드
                int maxGold = itemData.goldRange[1]; // 최대 골드
                testItem.SetData(itemData, minGold, maxGold);
            }
            else
            {
                testItem.SetData(itemData);
            }
        }
    }


    public void ResetStatus()
    {
        currentHealth = GameManager.Instance.DataManager.Creature.GetHealth(id);  // 최대 체력으로 리셋
       // _isDie = false;  // 죽지 않은 상태로 리셋
    }
}
