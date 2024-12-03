using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    MineData mineData;  // 몬스터 데이터
    GameManager gameManager;   // 게임 매니저
    MineFull minefull;
    private void Awake()
    {
        mineData = GetComponent<MineData>();  // MonsterData 컴포넌트 가져오기
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
