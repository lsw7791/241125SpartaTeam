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
    public List<int> creaturedropItemIds;   // ����� ������ ID ����Ʈ

    public int currentHealth;
    public bool isDie;

    // ���� �ʱ�ȭ
    public void Initialize(int idNum)
    {
        this.id = idNum;

        // DataManager���� �����͸� �������� ���� �Ӽ� �ʱ�ȭ
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

    // Ǯ�� ��ȯ�� �� ���� ����
    public void ResetStatus()
    {
        currentHealth = creatureHealth;  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
}
