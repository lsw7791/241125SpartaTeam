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

    // ���� �ʱ�ȭ
    public void Initialize(int creatureId)
    {
        this.creatureid = creatureId;

        // DataManager���� �����͸� �������� ���� �Ӽ� �ʱ�ȭ
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

    // Ǯ�� ��ȯ�� �� ���� ����
    public void ResetStatus()
    {
        currentHealth = creatureHealth;  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
}