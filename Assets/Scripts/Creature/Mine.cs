using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICreature
{
    
    GameManager gameManager;   // 게임 매니저
    MineFull minefull;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isDie;
    [SerializeField] public int id;

    private void Awake()
    {
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
            if (isDie == true)
            {
                minefull.ObjectSetActive(true);
                isDie = false;
                ResetStatus();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Weapon"))
        {
            // Weapon 스크립트에서 id 값을 가져오기
            PlayerWeapon playerWeapon = collision.collider.GetComponent<PlayerWeapon>();

            // 데미지를 받는 메서드 호출
            TakeDamage(Player.Instance.Stats.Damage);
        }
    }
    public void TakeDamage(int damage)
    {       
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }
    public void Die()
    {
        isDie = true;  // 몬스터 죽음 처리
        minefull.ObjectSetActive(false);

        DropItems();  // 아이템 드랍 함수 호출
    }
    public void DropItems()
    {

        foreach (int itemId in DataManager.Instance.creature.GetDropItemIds(id))
        {
            var itemData = DataManager.Instance.GetItemDataById(itemId);

            if (itemData != null)
            {
                // 아이템 프리팹 로드
                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabsPath);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

                    // 아이템 스프라이트 로드
                    Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);
                    // 아이템 데이터 설정
                    TestItem testItem = item.GetComponent<TestItem>();
                    if (testItem != null)
                    {
                        testItem.SetData(itemData, itemSprite);  // 아이템 데이터와 스프라이트 설정
                    }
                }
            }

        }
    }
    public void ResetStatus()
    {
        currentHealth = DataManager.Instance.creature.GetHealth(id);  // 최대 체력으로 리셋
        isDie = false;  // 죽지 않은 상태로 리셋
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// 스텟 초기화
    }
}
