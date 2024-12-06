using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터 저장
    public string imagePath; // 아이템의 아이콘 경로 (이미지 경로)

    // 아이템 데이터를 설정하는 메서드
    public void SetData(ItemData data, string iconPath)
    {
        itemData = data;
        imagePath = iconPath; // 이미지 경로 저장
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와의 충돌인지 확인
        if (collision.CompareTag("Player"))
        {
            // 아이템의 이미지를 경로를 통해 로드
            Sprite itemSprite = LoadItemIcon(imagePath);

            // 아이템을 인벤토리에 추가
            GameManager.Instance.player.AddItemToInventory(
                itemData.id.ToString(),
                itemData.name,
                1,
                itemData.itemType.ToString(),
                itemSprite
            );

            Destroy(gameObject); // 아이템 제거
        }
    }

    // 아이콘을 경로로 로드하는 함수
    private Sprite LoadItemIcon(string imagePath)
    {
        // 경로가 비어있으면 null 반환
        if (string.IsNullOrEmpty(imagePath))
        {
            return null;
        }

        // Resources 폴더 내에서 아이콘 로드
        Sprite icon = Resources.Load<Sprite>(imagePath);

        if (icon == null)
        {
            Debug.LogWarning($"아이콘을 로드할 수 없습니다: {imagePath}");
        }

        return icon;  // 로드된 아이콘 반환
    }
}
