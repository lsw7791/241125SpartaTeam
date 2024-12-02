using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    MonsterData monsterData;
    public float currentID;
    public CreatureType currentType;
    public string currentName;
    public string currentDesc;
    public int currentTier;
    public int currentAttack;
    public int currentDefense;
    public int currentHealth;
    public int MaxHealth;
    public float currentAttackSpeed;
    public float currentDetectionRange;
    public float currentAttackRange;
    public float currentMoveSpeed;
    public float currentDrop;

    bool isDie;
    private void Awake()
    {
        monsterData.Initialize(1);
    }
    private void Start()
    {
        SetMonsterData(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HitDamage(1);
        }
    }
    public void SetMonsterData(int monsterid)
    {
        currentID = monsterData.id;
        currentType = monsterData.creatureType;
        currentName = monsterData.creatureName;
        currentDesc = monsterData.creatureDesc;
        currentTier = monsterData.creatureTier;
        currentAttack = monsterData.creatureAttack;
        currentDefense = monsterData.creatureDefense;
        currentHealth = monsterData.creatureHealth;
        currentAttackSpeed = monsterData.creatureAttackSpeed;
        currentDetectionRange = monsterData.creatureDetectionRange;
        currentAttackRange = monsterData.creatureAttackRange;
        currentMoveSpeed = monsterData.creatureMoveSpeed;
        currentDrop = monsterData.creatureDrop;
        isDie = true;
        MaxHealth = currentHealth;
    }
    public void IsDie()
    {
           //오브젝트풀로 반환

        isDie=true;
    }
    public void HitDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            IsDie();
        }
    }
}
