using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    MineData mineData;  // ���� ������
    GameManager gameManager;   // ���� �Ŵ���
    MineFull minefull;
    private void Awake()
    {
        mineData = GetComponent<MineData>();  // MonsterData ������Ʈ ��������
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
            minefull.ObjectSetActive(true);
            mineData.isDie = false;
        }
    }
    public void IsDie()
    {
    }
    public void GetDamaged(int damage)
    {
        int temp = mineData.creatureDefense - damage;
        if (temp >= 0) return;
        mineData.currentHealth -= temp;
        if(mineData.currentHealth <= 0)
        {
            mineData.isDie = true;
            minefull.ObjectSetActive(false);
        }
    }
}
