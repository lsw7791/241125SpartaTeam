using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // ���� UI ���

    public Transform slotContainer; // ���� �θ� ��ü
    public GameObject slotPrefab;   // ���� ������

    public Sprite emptySlotSprite;  // �� ���� �̹���

    private void Awake()
    {
        Setup(Player.Instance.inventory);
    }
    public void Setup(Inventory inventory)
    {

        Player.Instance.inventory.OnInventoryChanged += Refresh;

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

        var items = Player.Instance.inventory.GetItems(); // ������ ����Ʈ �޾ƿ���

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

}
