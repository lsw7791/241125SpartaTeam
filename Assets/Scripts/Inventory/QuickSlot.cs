using System.Collections.Generic;
using System;

[Serializable]
public class QuickSlot
{
    public List<QuickSlotItem> Slots;

    public QuickSlot()
    {
        Slots = new List<QuickSlotItem>();  // ���� �ʱ�ȭ
    }

    // �����Կ� ������ �߰�
    public void AddQuickSlotItem(string itemID, int slotIndex)
    {
        // �������� �̹� ���Կ� �ִ��� Ȯ��
        QuickSlotItem existingItem = Slots.Find(item => item.ItemID == itemID);
        if (existingItem == null)
        {
            QuickSlotItem newItem = new QuickSlotItem(itemID, slotIndex);
            Slots.Add(newItem);
        }
    }

    // �����Կ��� ������ ����
    public void RemoveQuickSlotItem(int slotIndex)
    {
        QuickSlotItem item = Slots.Find(i => i.SlotIndex == slotIndex);
        if (item != null)
        {
            Slots.Remove(item);
        }
    }

    // ���� �ε����� ������ ��������
    public QuickSlotItem GetSlotItem(int slotIndex)
    {
        return Slots.Find(item => item.SlotIndex == slotIndex);
    }
}

[Serializable]
public class QuickSlotItem
{
    public string ItemID;  // ������ ID
    public int SlotIndex;  // ���� ��ȣ

    public QuickSlotItem(string itemID, int slotIndex)
    {
        ItemID = itemID;
        SlotIndex = slotIndex;
    }
}
