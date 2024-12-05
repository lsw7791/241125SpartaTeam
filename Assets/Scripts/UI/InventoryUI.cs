using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private Inventory _inventory; // �κ��丮 ����
    private List<GameObject> _slots = new List<GameObject>(); // ���� UI ���

    public Transform slotContainer; // ���� �θ� ��ü
    public GameObject slotPrefab;   // ���� ������

    public Sprite emptySlotSprite;  // �� ���� �̹���

    /// <summary>
    /// UI�� �ʱ�ȭ�ϰ�, �κ��丮 ���� �̺�Ʈ�� �����մϴ�.
    /// </summary>
    /// <param name="inventory">�÷��̾��� �κ��丮</param>
    public void Setup(Inventory inventory)
    {
        // ���� �̺�Ʈ ���� ���� (�ߺ� ����)
        if (_inventory != null)
        {
            _inventory.OnInventoryChanged -= Refresh;
        }

        // ���ο� �κ��丮 ���� �� �̺�Ʈ ����
        _inventory = inventory;
        _inventory.OnInventoryChanged += Refresh;

        // ���� ���� �� �ʱ�ȭ
        InitializeSlots();
        Refresh(); // �ʱ� UI ����
    }

    /// <summary>
    /// �κ��丮 UI ������ �����մϴ�.
    /// </summary>
    private void InitializeSlots()
    {
        // ���� ���� ����
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        // ���� ����
        for (int i = 0; i < 12; i++) // ���� �� ���� (�ʿ�� ���� ����)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    /// <summary>
    /// �κ��丮 ���濡 ���� UI�� �����մϴ�.
    /// </summary>
    private void Refresh()
    {
        if (_inventory == null || _slots == null)
            return;

        var items = _inventory.GetItems(); // ������ ����Ʈ �޾ƿ���

        for (int i = 0; i < _slots.Count; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < items.Count)
            {
                // �������� �ִ� ���, ���Կ� ������ ���� ǥ��
                slotComponent.Initialize(items[i]);
            }
            else
            {
                // �� ���� ó��
                slotComponent.ClearSlot(emptySlotSprite);
            }
        }
    }

    /// <summary>
    /// ������Ʈ �ı� �� �̺�Ʈ ������ �����մϴ�.
    /// </summary>
    private void OnDestroy()
    {
        if (_inventory != null)
        {
            _inventory.OnInventoryChanged -= Refresh;
        }
    }
}
