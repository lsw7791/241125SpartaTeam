using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public Dictionary<ItemType, Equip> equipItems = new Dictionary<ItemType, Equip>();
    [SerializeField] private EquipmentUI _equipmentUI;

    public void EquipNew(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        if (itemData.itemType > ItemType.Mine)
        {
            return;
        }

        // ���� ��� ����
        UnEquip(itemData.itemType);

        // �� ��� ����
        GameObject newEquip = new GameObject(itemData.name);
        //GameObject newEquip = Instantiate(Resources.Load<GameObject>(itemData.prefabPath));
        //newEquip.name = itemData.name;
        newEquip.transform.SetParent(transform);

        Equip curEquip = newEquip.AddComponent<Equip>();
        equipItems[itemData.itemType] = curEquip;

        // UI ���â ������Ʈ
        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, inItem.ItemIcon);

        inItem.IsEquipped = true;
    }

    public void UnEquip(ItemType itemType)
    {
        if (equipItems.ContainsKey(itemType))
        {
            Destroy(equipItems[itemType].gameObject);
            equipItems.Remove(itemType);

            // UI ���â Ŭ����
            _equipmentUI.ClearEquipmentSlot(itemType);

            // �κ��丮 ���� ���� ó��
            foreach (var item in GameManager.Instance.Player.inventory.Items)
            {
                var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);
                if (itemData != null && itemData.itemType == itemType)
                {
                    item.IsEquipped = false;
                }
            }
        }
    }

    public void EquipmentUIReference(EquipmentUI equipmentUI)
    {
        _equipmentUI = equipmentUI;
    }
}
