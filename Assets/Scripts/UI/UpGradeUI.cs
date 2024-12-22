using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;

    [SerializeField] private Button _upgradeResultButton; // 강화 버튼

    [SerializeField] private TMP_Text _productText;
    [SerializeField] private TMP_Text[] _probabilityText;

    [SerializeField] private TMP_Text _ResultTestText;
    [SerializeField] private TMP_Text _costText;

    InventoryItem item;

    private void Start()
    {
        _upgradeResultButton.onClick.AddListener(() =>
        {
            ShowUpgrade(item, item.enhenceCount);
        });
    }

    public void ShowUpgrade(InventoryItem inItem, int inEnhence)
    {
        if (inItem.enhenceCount > 10)
        {
            Debug.Log("최대 강화 수치입니다.");
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // 아이템 데이터 검색
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        // 강화 데이터 검색
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inEnhence);

        if (inItem.enhenceCount == 10)
        {
            Debug.Log("최대 강화 수치입니다.");
            _productText.text = $"{itemData.name} (+Max)";
            _probabilityText[0].text = $"성공 확률\n0%";
            _probabilityText[1].text = $"유지 확률\n0%";
            _probabilityText[2].text = $"파괴 확률\n0%";
            _ResultTestText.text = "최고 레벨 입니다";
            _costText.text = $"강화 비용: 0";
            return;
        }

        // 강화 확률 표시

        _productText.text = $"{itemData.name} (+{inEnhence})";
        _probabilityText[0].text = $"성공 확률\n{upgradeData.success[itemData.tier]}%";
        _probabilityText[1].text = $"유지 확률\n{upgradeData.fail[itemData.tier]}%";
        _probabilityText[2].text = $"파괴 확률\n{upgradeData.Destruction[itemData.tier]}%";
        _costText.text = $"강화 비용: {upgradeData.Cost[itemData.tier]}";
        int a = Random.Range(0, 100); // TODO :: 가중치로 바꾸기 전 테스트용 랜덤 값
        if (upgradeData.success[itemData.tier] > a)
        {
            inItem.enhenceCount++;
            _ResultTestText.text = "성공";
        }
        else
        {
            _ResultTestText.text = "실패";
        }
    }

    public void Initialize(InventoryItem inItem)
    {
        item = inItem;

        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        _useItemImage.sprite = inItem.ItemIcon;
        _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
        _probabilityText[0].text = $"성공 확률\n{upgradeData.success[itemData.tier]}%";
        _probabilityText[1].text = $"유지 확률\n{upgradeData.fail[itemData.tier]}%";
        _probabilityText[2].text = $"파괴 확률\n{upgradeData.Destruction[itemData.tier]}%";
        _ResultTestText.text = "결과";
        _costText.text = $"강화 비용: {upgradeData.Cost[itemData.tier]}";
    }
}
