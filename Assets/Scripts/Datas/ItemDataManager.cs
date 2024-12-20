using System.Collections.Generic;
using UnityEngine;
using MainData;
using GoogleSheet.Core.Type;

// ���� ��Ʈ�� "ItemType" ���� ���εǴ� ������
[UGS(typeof(ItemType))]
public enum ItemType
{
    Weapon = 1,         // ��
    Shield,            // ����
    Pickaxe,           // ���
    Helmet,            // ���
    Top,               // ����
    Bottom,            // ����
    Armor,             // ����
    Cape,              // ����
    Mine,              // ����
    Jewel,             // ����
    Ladder,            // ��ٸ�
    Other,             // ��Ÿ
    Gold,              // ��ȭ
    Potion,      //����
}

//public enum ItemUseType
//{ // �κ��丮���� ������ ������ �� ������ Ÿ��
//    Consumable,
//    Equipment,
//    Material,
//}

// ������ �����͸� �����ϴ� �Ŵ��� Ŭ����
public class ItemDataManager : ItemData
{
    // ������ ������ ����Ʈ�� ��ȯ
    public List<ItemData> GetItemDatas()
    {
        return ItemDataList;
    }

    // Ư�� ID�� ������ �����͸� ��ȯ
    public ItemData GetData(int id)
    {
        if (ItemDataMap.ContainsKey(id))
        {
            return ItemDataMap[id];
        }
        return null;
    }

    // Ư�� Ÿ��(ItemType)�� ��� ������ �����͸� ��ȯ
    public List<ItemData> GetDataByType(ItemType type)
    {
        return ItemDataList.FindAll(x => x.itemType == type);
    }

    // ������ ����Ͽ� �÷��̾��� HP�� ���¹̳ʸ� ������Ŵ
    public void UsePotion(int itemId)
    {
        // ���� ������ �����͸� ������
        ItemData potionData = GetData(itemId);

        // �������� null�̰ų� ������ �ƴ� ���
        if (potionData == null || potionData.itemType != ItemType.Potion)
        {
            Debug.LogError("Invalid item ID or not a Potion.");
            return;
        }

        // ������ ȿ�� (Health�� Stamina ��) ��������
        int healthIncrease = potionData.health;
        int staminaIncrease = potionData.stamina;

        // �÷��̾��� HP�� Stamina ������Ʈ
        var playerStats = GameManager.Instance.Player.stats;

        playerStats.CurrentHP = Mathf.Min(playerStats.MaxHP, playerStats.CurrentHP + healthIncrease);
        playerStats.CurrentStamina = Mathf.Min(playerStats.MaxStamina, playerStats.CurrentStamina + staminaIncrease);

        // �����: ȿ�� ���� �� �� Ȯ��
        Debug.Log($"Potion used: Health +{healthIncrease}, Stamina +{staminaIncrease}");
        Debug.Log($"Player stats updated: HP = {playerStats.CurrentHP}, Stamina = {playerStats.CurrentStamina}");
    }
}

