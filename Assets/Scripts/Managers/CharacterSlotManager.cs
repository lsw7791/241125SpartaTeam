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
            Debug.LogWarning("슬롯이 모두 가득 찼습니다.");
            return;
        }

        slots.Add(newPlayer);
        SaveSlotsData();
        Debug.Log($"캐릭터 {newPlayer.NickName} 추가 완료!");
    }

    public List<PlayerData> GetAllSlotData()
    {
        return slots;
    }

    public void SaveSlotsData()
    {
        repository.SavePlayerData(slots);
        Debug.Log("슬롯 데이터 저장 완료!");
    }

    public void LoadSlotsData()
    {
        slots = repository.LoadPlayerData();
        Debug.Log("슬롯 데이터 로드 완료!");
    }
}


