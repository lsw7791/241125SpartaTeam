using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;
    [SerializeField] private Image _backgroundImage;

    [SerializeField] private Button _upgradeResultButton; // 강화 버튼

    [SerializeField] private TMP_Text _productText;
    [SerializeField] private TMP_Text[] _probabilityText;

    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _costText;

    private InventoryItem _item;

    private void Start()
    {
        _upgradeResultButton.onClick.AddListener(() =>
        {
            ShowUpgrade(_item);
        });
    }

    public void ShowUpgrade(InventoryItem inItem)
    {
        if (inItem.enhenceCount >= 10)
        {
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // 아이템 데이터 검색
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierIndex = itemData.tier - 1;

        // 강화 데이터 검색
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        // 랜덤 가중치
        int randomValue = Random.Range(0, 100);

        if (randomValue < upgradeData.success[tierIndex])
        {
            // TODO :: 강화된 능력치 출력
            // TODO :: 강화수치 이미지 출력
            if (inItem.IsEquipped)
            {
                GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, false);
                inItem.enhenceCount++;
                GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, true);
            }
            else
            {
                inItem.enhenceCount++;
            }

            _resultText.text = "강화에 성공하였습니다!";
        }
        else if (randomValue < upgradeData.success[tierIndex] + upgradeData.fail[tierIndex])
        {
            // TODO :: 유지된 능력치 출력
            _resultText.text = "강화에 실패하였습니다.";
        }
        else
        {
            // TODO :: 금간 장비 이미지? 깨진 이미지 출력
            _resultText.text = "강화에 실패하였습니다. 장비가 파괴되었습니다.";
        }

        UpdateUI(inItem);
    }

    public void Initialize(InventoryItem inItem)
    {
        _item = inItem;
        _useItemImage.sprite = inItem.ItemIcon;
        UpdateUI(inItem);
    }

    private void UpdateUI(InventoryItem inItem) // UI 업데이트
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);
        int tierIndex = itemData.tier - 1;

        if (inItem.enhenceCount >= 10)
        {
            _productText.text = $"{itemData.name} (+Max)";
            _probabilityText[0].text = "성공 확률\n0%";
            _probabilityText[1].text = "유지 확률\n0%";
            _probabilityText[2].text = "파괴 확률\n0%";
            _costText.text = $"강화 비용: 0\n/\n현재 소지금";
        }
        else
        {
            _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
            _probabilityText[0].text = $"성공 확률\n{upgradeData.success[tierIndex]}%";
            _probabilityText[1].text = $"유지 확률\n{upgradeData.fail[tierIndex]}%";
            _probabilityText[2].text = $"파괴 확률\n{upgradeData.Destruction[tierIndex]}%";
            _costText.text = $"강화 비용: {upgradeData.Cost[tierIndex]}\n/\n현재 소지금";
        }
    }
}
