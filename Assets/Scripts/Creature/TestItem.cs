using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터 저장
    public Sprite itemSprite; // 아이템의 스프라이트 (이미지)

    // 아이템 데이터를 설정하는 메서드
    public void SetData(ItemData data, Sprite sprite)
    {
        itemData = data;
        itemSprite = sprite; // 스프라이트 데이터 저장
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와의 충돌인지 확인
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Player.AddItemToInventory(
                itemData.id,
                itemData.name,
                1,
                itemData.itemType,
                itemSprite
            );

            Destroy(gameObject); // 아이템 제거
        }
    }

}
