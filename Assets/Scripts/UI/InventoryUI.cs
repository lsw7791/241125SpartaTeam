using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private const int TotalSlots = 12; // ���� ���� ����
    private List<ItemInstance> _items;
    private List<GameObject> _slots;

    public Transform slotContainer; // ���� �θ� ��ü
    public GameObject slotPrefab;   // ���� ������

    public Sprite emptySlotSprite;  // �� ���Կ� ����� �̹���

    public void Initialize(List<ItemInstance> items)
    {
        _items = items;
        CreateSlots();  // ������ ���� ����
        Refresh();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < TotalSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    private void Refresh()
    {
        // ���� �ʱ�ȭ
        for (int i = 0; i < TotalSlots; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < _items.Count)
            {
                // �������� �ִ� ��� ���Կ� ������ ǥ��
                slotComponent.Initialize(_items[i]);
            }
            else
            {
                // �������� ���� ��� �� �������� ����
                slotComponent.ClearSlot(emptySlotSprite);
            }
        }
    }
}
