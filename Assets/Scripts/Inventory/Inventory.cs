using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // 플레이어의 인벤토리 아이템 목록
    public event Action OnInventoryChanged; // 인벤토리 변경 이벤트

    public Inventory()
    {
        Items = new List<InventoryItem>();
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
                GameManager.Instance.GetCurrentCharacter().AddItem(newItem); // PlayerData에도 추가
            }
        }

        OnInventoryChanged?.Invoke();
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
                GameManager.Instance.GetCurrentCharacter().RemoveItem(item); // PlayerData에서 제거
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
            // 장착 시 플레이어 스탯 갱신
            //PlayerData playerData = GameManager.Instance.Player.stats;
            //playerData.PlayerStatsEquip(item);
            //item.IsEquipped = true;  // 아이템 장착 상태 업데이트

            //GameManager.Instance.Player.stats.PlayerStatsUpdate(item, true);
            //item.IsEquipped = true;
            //GameManager.Instance.Player.stats.AddItem(item); // PlayerData에도 추가
            OnInventoryChanged?.Invoke();
        }
    }

    // 아이템 해제 메서드
    public void UnEquipItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem item = GetItem(itemID);

        if (item != null && itemData != null)
        {
            // 해제 시 플레이어 스탯 갱신
            //PlayerData playerData = GameManager.Instance.Player.stats;
            //playerData.PlayerStatsUnEquip(item);
            //item.IsEquipped = false;  // 아이템 해제 상태 업데이트

            // 스탯 감소 처리
            //GameManager.Instance.Player.stats.PlayerStatsUpdate(item, false);
            //item.IsEquipped = false;
            //GameManager.Instance.Player.stats.RemoveItem(item); // PlayerData에서 제거

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
}
