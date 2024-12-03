using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    MonsterData monsterData;  // ���� ������
    GameManager gameManager;   // ���� �Ŵ���
    MineFull minefull;
    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData ������Ʈ ��������
        gameManager = GameManager.Instance;  // ���� �Ŵ��� �ν��Ͻ� ��������
        minefull = GetComponentInChildren<MineFull>();
    }
    private void Start()
    {
        // 30�ʸ��� ObjectSetActive ȣ��
        StartCoroutine(ActivateObjectEvery30Seconds());
    }
    private IEnumerator ActivateObjectEvery30Seconds()
    {
        while (true)
        {
            // 30�� ��ٸ� ��
            yield return new WaitForSeconds(30f);

            // SetActive ȣ��
            if (monsterData.isDie == true)
            {
                minefull.ObjectSetActive(true);
                monsterData.isDie = false;
            }
        }
    }
    public void IsDie()
    {
    }
    public void GetDamaged(int damage)
    {
        int temp = monsterData.creatureDefense - damage;
        if (temp >= 0) return;
        monsterData.currentHealth -= temp;
        if(monsterData.currentHealth <= 0)
        {
            monsterData.isDie = true;
            minefull.ObjectSetActive(false);
        }
    }
}
