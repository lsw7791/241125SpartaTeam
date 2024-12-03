using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    MonsterData monsterData;  // 몬스터 데이터
    GameManager gameManager;   // 게임 매니저
    MineFull minefull;
    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData 컴포넌트 가져오기
        gameManager = GameManager.Instance;  // 게임 매니저 인스턴스 가져오기
        minefull = GetComponentInChildren<MineFull>();
    }
    private void Start()
    {
        // 30초마다 ObjectSetActive 호출
        StartCoroutine(ActivateObjectEvery30Seconds());
    }
    private IEnumerator ActivateObjectEvery30Seconds()
    {
        while (true)
        {
            // 30초 기다린 후
            yield return new WaitForSeconds(30f);

            // SetActive 호출
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
