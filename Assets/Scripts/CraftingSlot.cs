using MainData;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;  // 아이템 이미지

    // 슬롯에 아이템 데이터 적용하는 메소드
    public void Setup(CraftingData data)
    {
        // 이미지 경로로 Sprite 로드
        Sprite sprite = Resources.Load<Sprite>(data.imagePath);
        if (sprite != null)
        {
            itemImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("아이템 이미지 로드 실패: " + data.imagePath);
        }
    }
}