using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineData : ICreature
{
    public int monsterid;
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

    public bool isDie;
    public void Initialize(int creatureid)
    {
        monsterid = DataManager.Instance.creature.GetData(creatureid).id;
        creatureType = DataManager.Instance.creature.GetData(creatureid).creatureType;
        creatureName = DataManager.Instance.creature.GetData(creatureid).name;
        creatureDesc = DataManager.Instance.creature.GetData(creatureid).desc;
        creatureTier = DataManager.Instance.creature.GetData(creatureid).tier;
        creatureAttack = DataManager.Instance.creature.GetData(creatureid).attack;
        creatureDefense = DataManager.Instance.creature.GetData(creatureid).defense;
        creatureHealth = DataManager.Instance.creature.GetData(creatureid).health;
        creatureAttackSpeed = DataManager.Instance.creature.GetData(creatureid).attackSpeed;
        creatureDetectionRange = DataManager.Instance.creature.GetData(creatureid).detectionRange;
        creatureAttackRange = DataManager.Instance.creature.GetData(creatureid).attackRange;
        creatureMoveSpeed = DataManager.Instance.creature.GetData(creatureid).moveSpeed;
        creatureDrop = DataManager.Instance.creature.GetData(creatureid).drop;

        isDie = true;
    }
}
