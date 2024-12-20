using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;
    
    [SerializeField] private Button _craftResultButton; // 조합 버튼

    InventoryItem item;

    private void Start()
    {
        _craftResultButton.onClick.AddListener(() =>
        {
            ShowUpgradeChance(item, item.enhenceCount);
        });
    }

    public void ShowUpgradeChance(InventoryItem inItem, int inEnhence)
    {
        if (inItem.enhenceCount == 10)
        {
            Debug.Log("최대 강화 수치입니다.");
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // 아이템 데이터 검색
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        // 강화 데이터 검색
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetUpgradeData(inEnhence, itemData.tier);

        // 강화 확률 표시
        Debug.Log($"아이템: {itemData.name} (티어 {itemData.tier})");
        Debug.Log($"강화 단계: {inEnhence}");
        Debug.Log($"성공 확률: {upgradeData.success}%");
        Debug.Log($"실패 확률: {upgradeData.fail}%");
        Debug.Log($"파괴 확률: {upgradeData.Destruction}%");

        inItem.enhenceCount++;
    }

    public void Initialize(InventoryItem inItem)
    {
        item = inItem;

        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        _useItemImage.sprite = inItem.ItemIcon;
    }
}
