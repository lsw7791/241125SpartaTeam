using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICreature
{
    
    GameManager gameManager;   // 게임 매니저
    MineFull minefull;
    [SerializeField] public int currentHealth;
    [SerializeField] private bool isDie;
    [SerializeField] public int id;
    [SerializeField] private MineHealthBarUI mineHealthBarUI;
    private void Awake()
    {
        gameManager = GameManager.Instance;  // 게임 매니저 인스턴스 가져오기
        minefull = GetComponentInChildren<MineFull>();
    }
    private void Start()
    {
        mineHealthBarUI = GetComponent<MineHealthBarUI>();
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
            if (isDie == true)
            {
                minefull.ObjectSetActive(true);
                isDie = false;
                ResetStatus();
            }
        }
    }
    public void TakeDamage(int damage)
    {
        int value = damage - GameManager.Instance.DataManager.Creature.GetDefense(id);
        if (value > 0)
        {
            currentHealth -= value;
            mineHealthBarUI.UpdateHealthBar();
        if (currentHealth <= 0) Die();
        }
    }
    public void Die()
    {
        isDie = true;  // 몬스터 죽음 처리
        minefull.ObjectSetActive(false);

        DropItems();  // 아이템 드랍 함수 호출
    }
    public void DropItems()
    {

        foreach (int itemId in GameManager.Instance.DataManager.Creature.GetDropItemIds(id))
        {
            var itemData = GameManager.Instance.DataManager.GetItemDataById(itemId);

            if (itemData != null)
            {
                // 아이템 프리팹 로드
                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

                    // 아이템 데이터 설정
                    TestItem testItem = item.GetComponent<TestItem>();
                    if (testItem != null)
                    {
                        testItem.SetData(itemData);  // 아이템 데이터와 스프라이트 설정
                    }
                }
            }

        }
    }
    public void ResetStatus()
    {
        currentHealth = GameManager.Instance.DataManager.Creature.GetHealth(id);  // 최대 체력으로 리셋
        isDie = false;  // 죽지 않은 상태로 리셋
        if(mineHealthBarUI==null)
        {
            mineHealthBarUI = GetComponent<MineHealthBarUI>();
        }
        mineHealthBarUI.UpdateHealthBar();
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// 스텟 초기화
    }
}
