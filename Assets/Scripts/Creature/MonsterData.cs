using MainData;
using UnityEngine;

public class MonsterData : MonoBehaviour, ICreature
{
    public int id;
    public int creatureid;
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
    public float creatureDrop;

    public int currentHealth;
    public bool isDie;

    // ���� �ʱ�ȭ
    public void Initialize(int creatureId)
    {
        this.creatureid = creatureId;

        // ���� ������ �ʱ�ȭ (creatureId�� ���� ������ ����)
        id = DataManager.Instance.creature.GetData(creatureId).id;
        creatureType = DataManager.Instance.creature.GetData(creatureId).creatureType;
        creatureName = DataManager.Instance.creature.GetData(creatureId).name;
        creatureDesc = DataManager.Instance.creature.GetData(creatureId).desc;
        creatureTier = DataManager.Instance.creature.GetData(creatureId).tier;
        creatureAttack = DataManager.Instance.creature.GetData(creatureId).attack;
        creatureDefense = DataManager.Instance.creature.GetData(creatureId).defense;
        creatureHealth = DataManager.Instance.creature.GetData(creatureId).health;
        creatureAttackSpeed = DataManager.Instance.creature.GetData(creatureId).attackSpeed;
        creatureDetectionRange = DataManager.Instance.creature.GetData(creatureId).detectionRange;
        creatureAttackRange = DataManager.Instance.creature.GetData(creatureId).attackRange;
        creatureMoveSpeed = DataManager.Instance.creature.GetData(creatureId).moveSpeed;
        creatureDrop = DataManager.Instance.creature.GetData(creatureId).drop;

        currentHealth = creatureHealth;
        isDie = false;
    }

    // Ǯ�� ��ȯ�� �� ���� ����
    public void ResetStatus()
    {
        currentHealth = creatureHealth;  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
}