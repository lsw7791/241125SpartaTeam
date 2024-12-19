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

        // 기존 장비 해제
        UnEquip(itemData.itemType);

        // 새 장비 장착
        //GameObject newEquip = new GameObject(itemData.name);
        //EquipManager.Instance.EquipedWeapon = Instantiate(Resources.Load<GameObject>(itemData.prefabPath));

        Equip curEquip = GameManager.Instance.Player.Weapon.AddComponent<Equip>();
        equipItems[itemData.itemType] = curEquip;

        // UI 장비창 업데이트
        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, inItem.ItemIcon);
        GameManager.Instance.Player._playerWeapon.ATKType = inItem.ATKType;

        inItem.IsEquipped = true;
    }

    public void UnEquip(ItemType itemType)
    {
        if (equipItems.ContainsKey(itemType))
        {
            // 장비 오브젝트의 SpriteRenderer를 null로 설정
            Equip currentEquip = equipItems[itemType];
            SpriteRenderer equipSpriteRenderer = currentEquip.GetComponentInChildren<SpriteRenderer>();

            if (equipSpriteRenderer != null)
            {
                equipSpriteRenderer.sprite = null; // 스프라이트 해제
            }

            equipItems.Remove(itemType);

            // UI 장비창 클리어
            _equipmentUI.ClearEquipmentSlot(itemType);

            // 인벤토리 상태 해제 처리
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
