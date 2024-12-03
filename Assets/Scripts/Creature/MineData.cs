using UnityEngine;

public class MineData : MonoBehaviour, ICreature
{
    public int id;
    public int creatureid;
    public CreatureType creatureType;
    public string creatureName;
    public string creatureDesc;
    public int creatureTier;
    public int creatureDefense;
    public int creatureHealth;
    public float creatureDrop;

    public int currentHealth;
    public bool isDie;

    // 광물 초기화
    public void Initialize(int creatureId)
    {
        this.creatureid = creatureId;

        // DataManager에서 데이터를 가져오고 몬스터 속성 초기화
        var data = DataManager.Instance.creature.GetData(creatureId);
        id = data.id;
        creatureType = data.creatureType;
        creatureName = data.name;
        creatureDesc = data.desc;
        creatureTier = data.tier;
        creatureDefense = data.defense;
        creatureHealth = data.health;
        creatureDrop = data.drop;

        currentHealth = creatureHealth;
        isDie = false;
    }

    // 풀로 반환될 때 상태 리셋
    public void ResetStatus()
    {
        currentHealth = creatureHealth;  // 최대 체력으로 리셋
        isDie = false;  // 죽지 않은 상태로 리셋
    }
}