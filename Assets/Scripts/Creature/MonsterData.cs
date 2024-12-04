using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour, ICreature
{
    public int id;
    public CreatureType creatureType;
    public string creatureName;
    public string creatureDesc;
    public int creatureTier;
    public int creatureAttack;
    public int creatureDefense;
    public int creatureHealth;
    public float creatureAttackSpeed;
    public float creatureDetectionRange;
    public float creatureAttackRange;
    public float creatureMoveSpeed;
    public List<int> creaturedropItemIds;   // 드랍할 아이템 ID 리스트

    public int currentHealth;
    public bool isDie;

    // 몬스터 초기화
    public void Initialize(int idNum)
    {
        this.id = idNum;

        // DataManager에서 데이터를 가져오고 몬스터 속성 초기화
        var data = DataManager.Instance.creature.GetData(idNum);
        id = data.id;
        creatureType = data.creatureType;
        creatureName = data.name;
        creatureDesc = data.desc;
        creatureTier = data.tier;
        creatureAttack = data.attack;
        creatureDefense = data.defense;
        creatureHealth = data.health;
        creatureAttackSpeed = data.attackSpeed;
        creatureDetectionRange = data.detectionRange;
        creatureAttackRange = data.attackRange;
        creatureMoveSpeed = data.moveSpeed;
        creaturedropItemIds = new List<int>(data.dropItemIds);
    }

    // 풀로 반환될 때 상태 리셋
    public void ResetStatus()
    {
        currentHealth = creatureHealth;  // 최대 체력으로 리셋
        isDie = false;  // 죽지 않은 상태로 리셋
    }
}
