using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : UIBase
{
    [SerializeField] private Transform itemListContainer;  // ������ ����Ʈ�� ǥ���� UI �����̳�
    [SerializeField] private GameObject itemSlotPrefab;    // ������ ������ ������

    private Inventory inventory;

    protected override void Awake()
    {
        base.Awake();
        inventory = Player.Instance.Inventory;  // �÷��̾��� �κ��丮 ����
    }

    public override void Open()
    {
        base.Open();  // UIBase�� Open �޼��� ȣ��
        UpdateInventoryUI();  // �κ��丮 UI ����
    }

    public override void Close()
    {
        base.Close();  // UIBase�� Close �޼��� ȣ��
    }

    // �κ��丮 UI ������Ʈ
    public void UpdateInventoryUI()
    {
        // ���� ������ ����Ʈ ����
        foreach (Transform child in itemListContainer)
        {
            Destroy(child.gameObject);
        }

        // ���� �κ��丮 �����۵��� ������ �������� ��ȯ�Ͽ� ǥ��
        List<InventoryItem> inventoryItems = inventory.GetItems();  // �κ��丮 ������ ��� ��������

        foreach (var item in inventoryItems)
        {
            // ���� ����
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemListContainer);

            // TextMeshProUGUI ������Ʈ�� ������ �̸��� ���� ǥ��
            TextMeshProUGUI itemText = itemSlot.GetComponentInChildren<TextMeshProUGUI>();  // TextMeshProUGUI�� ����
            if (itemText != null)
            {
                itemText.text = $"{item.ItemName} x{item.Quantity}";  // ������ �̸��� ����
            }

            // ������ �̹��� ������Ʈ�� ������ ������ ����
            Image itemIcon = itemSlot.GetComponentInChildren<Image>();
            if (itemIcon != null && item.ItemIcon != null)
            {
                itemIcon.sprite = item.ItemIcon;  // ������ ������ ����
            }
        }
    }
}
