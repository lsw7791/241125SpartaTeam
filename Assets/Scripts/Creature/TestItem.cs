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
        Debug.Log($"Item set: {itemData.name} - {itemData.desc}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와의 충돌인지 확인
        if (collision.CompareTag("Player"))
        {
            // 아이템을 인벤토리에 추가 (싱글톤을 활용)
            Player.Instance.AddItemToInventory(
                itemData.id.ToString(),
                itemData.name,
                1,
                itemData.itemType.ToString(),
                itemSprite
            );

            Destroy(gameObject); // 아이템 제거
        }
    }

}
