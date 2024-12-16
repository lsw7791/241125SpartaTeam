using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializationWrapper<T>
{
    public List<T> Items;

    public SerializationWrapper(List<T> items)
    {
        Items = items;
    }
}


public class CharacterSlotManager
{
    private List<PlayerData> slots = new List<PlayerData>();
    private IPlayerRepository repository;

    public CharacterSlotManager(IPlayerRepository repository)
    {
        this.repository = repository;
        LoadSlotsData();
    }

    public void AddCharacterToSlot(PlayerData newPlayer)
    {
        if (slots.Count >= 4)
        {
            Debug.LogWarning("������ ��� ���� á���ϴ�.");
            return;
        }

        slots.Add(newPlayer);
        SaveSlotsData();
        Debug.Log($"ĳ���� {newPlayer.NickName} �߰� �Ϸ�!");
    }

    public List<PlayerData> GetAllSlotData()
    {
        return slots;
    }

    public void SaveSlotsData()
    {
        repository.SavePlayerData(slots);
        Debug.Log("���� ������ ���� �Ϸ�!");
    }

    public void LoadSlotsData()
    {
        slots = repository.LoadPlayerData();
        Debug.Log("���� ������ �ε� �Ϸ�!");
    }
}


