using MainData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopSlot : MonoBehaviour
{
    // UI 요소들
    public Image icon; // 아이템 아이콘
    public TMP_Text name; // 아이템 이름 
    public TMP_Text desc; // 아이템 설명 
    public TMP_Text gold; // 아이템 가격 
    // 아이템 데이터 설정 메서드

    public void Setup(ItemData itemData)
    {
        // 아이템 데이터에 맞게 UI 요소들을 설정
        icon.sprite = Resources.Load<Sprite>(itemData.spritePath); // 아이콘 경로로부터 이미지 로드
        name.text = itemData.name; // 아이템 이름
        desc.text = itemData.desc; // 아이템 설명
        gold.text = $"{itemData.buy} 골드"; // 아이템 가격
    }

    public void OnclickedSlot()
    {
        GameManager.Instance.uIManager.ToggleUI<BuyUI>();
    }
}
