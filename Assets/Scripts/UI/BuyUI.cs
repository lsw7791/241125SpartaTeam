using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : UIBase
{
    [SerializeField] TMP_Text _text;        // 구매 확인 텍스트
    [SerializeField] GameObject _buyBtnObj; // 구매 버튼 오브젝트
    [SerializeField] Button _buyBtn;       // 구매 버튼
    [SerializeField] ItemData _itemData;

    public void SetUp(ItemData itemData)
    {
        _itemData = itemData; // 아이템 데이터 설정

        // 골드와 아이템 가격 비교
        bool canBuy = GameManager.Instance.player.stats.Gold >= itemData.buy;

        if (canBuy)
        {
            _text.text = "구매하시겠습니까?";
            ToggleBuyButton(true); // 버튼 활성화
            _buyBtn.onClick.RemoveAllListeners(); // 기존 이벤트 제거
            _buyBtn.onClick.AddListener(() => PurchaseItem(itemData)); // 구매 이벤트 추가
        }
        else
        {
            ToggleBuyButton(false); // 버튼 비활성화
        }
    }

    private void ToggleBuyButton(bool isActive)
    {
        _buyBtnObj.SetActive(isActive);
        _buyBtn.interactable = isActive;
    }

    public void PurchaseItem(ItemData itemData)
    {
        // 골드 차감
        GameManager.Instance.player.stats.Gold -= itemData.buy;

        // 완료 메시지
        _text.text = $"{itemData.name}를(을) 구매하였습니다!";

        // 스프라이트 경로에서 Sprite 객체를 로드
        Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);

        // 아이템을 인벤토리에 추가
        GameManager.Instance.player.inventory.AddItem(
            itemData.id.ToString(),  // 아이템 ID를 문자열로 변환
            itemData.name,           // 아이템 이름
            1,                       // 아이템 수량 (구매 시 1개)
            itemData.itemType,           // 아이템 타입
            itemSprite               // 아이템 스프라이트
        );

        ToggleBuyButton(false); // 버튼 비활성화
    }

}
