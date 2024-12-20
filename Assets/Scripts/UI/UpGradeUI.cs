using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;
    
    [SerializeField] private Button _craftResultButton; // ���� ��ư

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
            Debug.Log("�ִ� ��ȭ ��ġ�Դϴ�.");
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // ������ ������ �˻�
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        // ��ȭ ������ �˻�
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetUpgradeData(inEnhence, itemData.tier);

        // ��ȭ Ȯ�� ǥ��
        Debug.Log($"������: {itemData.name} (Ƽ�� {itemData.tier})");
        Debug.Log($"��ȭ �ܰ�: {inEnhence}");
        Debug.Log($"���� Ȯ��: {upgradeData.success}%");
        Debug.Log($"���� Ȯ��: {upgradeData.fail}%");
        Debug.Log($"�ı� Ȯ��: {upgradeData.Destruction}%");

        inItem.enhenceCount++;
    }

    public void Initialize(InventoryItem inItem)
    {
        item = inItem;

        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        _useItemImage.sprite = inItem.ItemIcon;
    }
}
