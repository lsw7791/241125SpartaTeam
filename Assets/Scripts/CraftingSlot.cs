using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{ // 아이템 조합 슬롯의 UI를 관리
    [SerializeField] private Image itemImage; // 아이템 이미지 UI 요소
    [SerializeField] private Text itemName;   // 아이템 이름 UI 요소
    //[SerializeField] private Button craftButton; // 크래프트 버튼

    private CraftingData craftingData;

    private void OnEnable()
    {
        itemImage = GetComponent<Image>();
    }


    // 슬롯에 아이템 데이터를 설정하는 메서드
    //public void Setup(CraftingData data, Sprite itemSprite = null)
    //{ // 아이템 데이터를 UI 슬롯에 설정
    //    craftingData = data;

    //    // 아이템 이미지와 이름을 설정
    //    if (itemImage != null && itemSprite != null)
    //    {
    //        itemImage.sprite = itemSprite;
    //    }

    //    if (itemName != null)
    //    {
    //        itemName.text = craftingData != null ? craftingData.name : "아이템 없음";
    //    }
    //}

    // 크래프트 버튼 클릭 시 처리하는 메서드
    public void OnCraftButtonClick()
    { // 버튼 클릭 시, GameManager의 CraftingManager를 통해 아이템 선택을 처리
        // CraftingManager에서 아이템 선택 및 크래프트 시도
        if (craftingData != null)
        {
            GameManager.Instance.craftingManager.SelectItem(craftingData.id); // 아이템 선택
        }
    }
    // 현재 구현은 아이템 선택 단계
}
