using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellUI : UIBase
{
    [SerializeField] TMP_Text _text;        // 구매 확인 텍스트
    [SerializeField] GameObject _buyBtnObj; // 구매 버튼 오브젝트
    [SerializeField] Button _buyBtn;        // 구매 버튼
    [SerializeField] ItemData _itemData;
    [SerializeField] TMP_InputField _quantityInputField; // 수량 입력 필드
    [SerializeField] GameObject _inputFieldParent;

    public void SetUp(ItemData itemData)
    {
        _itemData = itemData; // 아이템 데이터 설정

        _text.text = "수량을 입력해주세요.";
        ToggleBuyButton(true); // 버튼 활성화          
        ToggleInputFieldParent(true); // 수량 입력 필드 부모 활성화

        _buyBtn.onClick.RemoveAllListeners(); // 기존 이벤트 제거
        _buyBtn.onClick.AddListener(() => PurchaseItem(itemData)); // 구매 이벤트 추가

    }

    private void ToggleBuyButton(bool isActive)
    {
        SoundManager.Instance.PlayButton1SFX();
        _buyBtnObj.SetActive(isActive);
        _buyBtn.interactable = isActive;
    }

    private void ToggleInputFieldParent(bool isActive)
    {
        _inputFieldParent.SetActive(isActive); // 수량 입력 필드 부모 객체 활성화/비활성화
    }
    private void UpdateGoldDisplay()
    {
        // 보유 골드 텍스트 업데이트
        //_hasGold.text = $"보유골드: {GameManager.Instance.player.stats.Gold}";
    }
    public void PurchaseItem(ItemData itemData)
    {
        bool isEquip = GameManager.Instance.Player.inventory.IsItemEquipped(itemData);
        if (isEquip)
        {
            _text.text = "착용중인 장비입니다.";
            return;
        }
        // 수량 입력값을 가져오기
        int quantity;

        if (!int.TryParse(_quantityInputField.text, out quantity))
        {
            // 숫자가 아닌 입력인 경우
            _text.text = "숫자로 입력해주세요.";
            return;
        }

        if (GameManager.Instance.Player.inventory.GetItemCount(itemData.id) < quantity)
        {
            quantity = GameManager.Instance.Player.inventory.GetItemCount(itemData.id);
        }

        quantity = Mathf.Max(quantity, 1); // 수량이 1 이상이 되도록

        // 총 금액 계산
        int totalCost = itemData.sell * quantity;
        // 골드 입금
        GameManager.Instance.Player.stats.Gold += totalCost;

        ShopUI shopUI;
        shopUI = UIManager.Instance.GetUI<ShopUI>();
        shopUI.HasGold.text = GameManager.Instance.Player.stats.Gold.ToString();

        // 아이템을 인벤토리에서 제거
        GameManager.Instance.Player.inventory.RemoveItem(itemData.id, quantity); // 수량 전달
        shopUI.SetupShopUI();

        ToggleBuyButton(false); // 버튼 비활성화
        ToggleInputFieldParent(false); // 수량 입력 필드 부모 비활성화
        _text.text = "판매가 완료되었습니다.";
        if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(6) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[6])
        {
            GameManager.Instance.DataManager.MainQuest.CompleteQuest(6);
        }
    }
}

