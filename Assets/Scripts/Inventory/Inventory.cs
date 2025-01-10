using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // 플레이어의 인벤토리 아이템 목록
    public event Action OnInventoryChanged; // 인벤토리 변경 이벤트

    private Dictionary<ItemType, InventoryItem> _equipItems = new Dictionary<ItemType, InventoryItem>();
    // 리스트로 생각해보기
    //public EquipmentUI _equipmentUI;
    //UI는 따로 빼기

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    public void SaveEquipInIt()
    {
        GameManager.Instance.Player._playerWeapon.ATKType = 0;
        GameManager.Instance.Player.Stats.WeaponType = 0;

        //InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
        //inventoryUI.EquipmentUIReset();

        foreach (InventoryItem item in Items)
        {
            if (item.IsEquipped)
            {
                var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);

                _equipItems[itemData.itemType] = item;

                // UI 장비창 업데이트
                //inventoryUI.UpdateEquipmentSlot(itemData.itemType, UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath));
                //inventoryUI.EquipmentRefresh();

                if (itemData.itemType == ItemType.Weapon)
                {
                    GameManager.Instance.Player._playerWeapon.ATKType = item.ATKType;
                    GameManager.Instance.Player.Stats.WeaponType = item.ATKType;
                }
            }
        }
    }

    // 인벤토리 초기화
    public void Clear()
    {
        Items.Clear();
        OnInventoryChanged?.Invoke();
    }

    // 아이템 추가 메서드
    public void AddItem(int itemID, int quantity)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);

        // 기존 로직: 장비 타입 구분하여 아이템 추가/수량 증가
        if (existingItem != null && itemData.itemType >= ItemType.Mine)
        {
            existingItem.Quantity += quantity; // 장비 아이템의 경우 수량 증가
        }
        else
        {
            if (itemData != null)
            {
                InventoryItem newItem = new InventoryItem(itemID, quantity, itemData.atlasPath, false, itemData.atkType);
                Items.Add(newItem); // 새 아이템 추가
            }
        }

        OnInventoryChanged?.Invoke();
        GameManager.Instance.DataManager.SaveData(this);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.stats);
        GameManager.Instance.Player.stats.nowMapNumber = GameManager.Instance.SceneNum;
    }


    // 아이템 조회 메서드
    public InventoryItem GetItem(int itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // 해당 ID의 아이템 반환
    }

    // 아이템 수량 조회
    public int GetItemCount(int itemID)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        return existingItem?.Quantity ?? 0; // 아이템 수량 반환, 없으면 0
    }

    // 아이템 목록 반환
    public List<InventoryItem> GetItems()
    {
        return Items;
    }

    // 아이템 제거 메서드
    public void RemoveItem(int itemID, int quantity)
    {
        InventoryItem item = Items.Find(i => i.ItemID == itemID);
        if (item != null)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0)
            {
                Items.Remove(item); // 인벤토리에서 제거
            }
        }

        OnInventoryChanged?.Invoke();
    }

    // 아이템 장착 메서드
    public void EquipItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem item = GetItem(itemID);

        if (item != null && itemData != null)
        {
            OnInventoryChanged?.Invoke();
        }
    }

    // 아이템 드롭 메서드
    public void DropItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem item = GetItem(itemID);

        if (item != null)
        {
            RemoveItem(itemID, 1); // 아이템 수량을 감소시키고

            // 아이템 데이터와 프리팹 로드
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);
            GameObject dropItem = GameObject.Instantiate(itemPrefab, GameManager.Instance.Player.transform.position, Quaternion.identity);
            if (dropItem.TryGetComponent<TestItem>(out var outItem))
            {
                outItem.isPlayerDrop = true;
                outItem.itemData = itemData;
            }

            OnInventoryChanged?.Invoke();
        }
    }

    // 인벤토리 아이템을 JSON 형식으로 저장
    public string SaveInventoryToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    // JSON 형식으로 저장된 인벤토리 데이터를 불러와서 인벤토리 복원
    public void LoadInventoryFromJson(string json)
    {
        Inventory loadedInventory = JsonUtility.FromJson<Inventory>(json);
        Items = loadedInventory.Items;  // 불러온 아이템 리스트로 교체
        OnInventoryChanged?.Invoke();
    }

    public void Equip(InventoryItem inItem)
    {
        var newItemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        if (newItemData.itemType > ItemType.Mine)
        {
            return;
        }

        // 같은 타입의 기존 장비가 있는지 확인
        if (_equipItems.TryGetValue(newItemData.itemType, out var equippedItem))
        {
            // 기존 장비 해제
            UnEquip(newItemData.itemType);
        }

        // 새 아이템 장착
        EquipNew(inItem);
    }

    public void EquipNew(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        // 기존 장비 해제
        UnEquip(itemData.itemType);


        _equipItems[itemData.itemType] = inItem;

        if (itemData.itemType == ItemType.Weapon)
        {
            GameManager.Instance.Player._playerWeapon.ATKType = inItem.ATKType;
            GameManager.Instance.Player.Stats.WeaponType = inItem.ATKType;
        }

        inItem.IsEquipped = true;
        GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, true);
    }

    public void UnEquip(ItemType inItemType)
    {
        if (inItemType == ItemType.Weapon)
        {
            GameManager.Instance.Player._playerWeapon.ATKType = 0;
            GameManager.Instance.Player.Stats.WeaponType = 0;
        }

        if (_equipItems.TryGetValue(inItemType, out var equippedItem))
        {
            // 스탯 감소 처리
            GameManager.Instance.Player.stats.PlayerStatsUpdate(equippedItem, false);

            //saveItemList.Remove(inItem);
            // 딕셔너리에서 제거
            _equipItems.Remove(inItemType);

            equippedItem.IsEquipped = false;
        }
    }
}
