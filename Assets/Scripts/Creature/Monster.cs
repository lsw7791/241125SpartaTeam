using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    MonsterData monsterData;  // ���� ������
    GameManager gameManager;   // ���� �Ŵ���

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData ������Ʈ ��������
        gameManager = GameManager.Instance;  // ���� �Ŵ��� �ν��Ͻ� ��������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        damageable.TakeDamage(monsterData.creatureAttack);  // ������ ó��
    }

    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void IsDie()
    {
        if (gameManager.monsterPool != null)
        {
            // ���͸� Ǯ�� ��ȯ (creatureId�� ����)
            gameManager.monsterPool.ReturnMonster(monsterData.creatureid, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        monsterData.isDie = true;  // ���� ���� ó��
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            IsDie();  // ü���� 0 ���ϰ� �Ǹ� ���� ó��
        }
    }
}