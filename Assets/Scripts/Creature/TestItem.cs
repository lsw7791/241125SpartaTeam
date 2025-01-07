using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터 저장
    //public Sprite itemSprite; // 아이템의 스프라이트 (이미지)
    private int minGold; // 최소 골드
    private int maxGold; // 최대 골드
    public bool isPlayerDrop = false;

    private void Start()
    {
        if(isPlayerDrop)
        {
            Invoke("PlayerDropItem", 5);
        }
    }

    private void PlayerDropItem()
    {
        isPlayerDrop = false;
    }

    // 아이템 데이터를 설정하는 메서드 (골드용)
    public void SetData(ItemData data, int minGold, int maxGold)
    {
        itemData = data;
        this.minGold = minGold;
        this.maxGold = maxGold;
    }

    // 아이템 데이터를 설정하는 메서드 (일반 아이템용)
    public void SetData(ItemData data)
    {
        itemData = data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerDrop)
        {
            if (itemData.itemType == ItemType.Gold) // 골드 아이템인지 확인
            {
                int randomGold = Random.Range(minGold, maxGold + 1); // 골드 범위에서 랜덤 값
                GameManager.Instance.Player.Stats.Gold += randomGold; // 플레이어 골드 추가
                Debug.Log($"{randomGold} 골드를 획득했습니다! 현재 골드: {GameManager.Instance.Player.Stats.Gold}");
            }
            else
            {
                // 일반 아이템은 인벤토리에 추가
                GameManager.Instance.Player.AddItemToInventory(
                    itemData.id,
                    1,
                    itemData.atlasPath
                );
                if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(7) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[7])
                {
                    // QuestCompletionStatus[1]이 false일 때만 CompleteQuest(1) 호출
                    GameManager.Instance.DataManager.MainQuest.CompleteQuest(7);
                }
            }

            Destroy(gameObject); // 아이템 제거
        }
    }
}
