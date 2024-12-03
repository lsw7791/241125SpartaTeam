using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    MonsterData monsterData;


    private void Awake()
    {
        monsterData =GetComponent<MonsterData>();
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

    }
    public void IsDie()
    {
        //������ƮǮ�� ��ȯ

        monsterData.isDie = true;
    }
    public void HitDamage(int damage)
    {
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            IsDie();
        }
    }
}
