using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // ���� UI ���

    public Transform slotContainer; // ���� �θ� ��ü
    public GameObject slotPrefab;   // ���� ������

    public Sprite emptySlotSprite;  // �� ���� �̹���

   //�κ��丮�� ���� �Ӷ����ٰ� �ƴ϶� �ǽð����� ������Ʈ�Ǵ� ����� �����Ǿ����
    private void OnEnable()
    {
        Setup(Player.Instance.inventory);
    }

    public void Setup(Inventory inventory)
    {
        // ���� �̺�Ʈ ��� ����
        Player.Instance.inventory.OnInventoryChanged -= Refresh;

        // �� �̺�Ʈ ���
        Player.Instance.inventory.OnInventoryChanged += Refresh;

        // ���� ���� �� �ʱ�ȭ
        InitializeSlots();
        Refresh(); // �ʱ� UI ����
    }

    private void InitializeSlots()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        for (int i = 0; i < 12; i++) // ���� �� ���� (�ʿ�� ���� ����)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    private void Refresh()
    {
        Debug.Log($"Inventory Refresh ȣ�� at {Time.time}");

        var items = Player.Instance.inventory.GetItems(); // ������ ����Ʈ �޾ƿ���

        for (int i = 0; i < _slots.Count; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < items.Count)
            {
                slotComponent.Initialize(items[i]);
            }
            else
            {
                slotComponent.ClearSlot();
            }
        }
    }
}
