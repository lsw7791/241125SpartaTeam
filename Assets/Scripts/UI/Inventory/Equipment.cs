using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public Dictionary<EquipSlot, Equip> equipItems = new Dictionary<EquipSlot, Equip>();


    public void EquipNew(InventoryItem inItem)
    {
        if (inItem.equipSlot == EquipSlot.None)
        {
            return;
        }
        // 기존 장비 해제
        UnEquip(inItem.equipSlot);
        // 새 장비 장착
        GameObject newEquip = new GameObject(inItem.ItemName);
        newEquip.transform.SetParent(transform);

        Equip curEquip = newEquip.AddComponent<Equip>();
        Image newImage = curEquip.AddComponent<Image>();
        newImage.sprite = inItem.ItemIcon;

        equipItems[inItem.equipSlot] = curEquip;
    }

    public void UnEquip(EquipSlot slot)
    {
        if (equipItems.ContainsKey(slot))
        {
            Destroy(equipItems[slot].gameObject);
            equipItems.Remove(slot);
        }
    }
}
