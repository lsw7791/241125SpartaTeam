using System.Collections.Generic;

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

    public void AddCharacterToSlot(PlayerData newPlayer)
    {
        if (slots.Count >= 4)
        {
            UnityEngine.Debug.LogWarning("슬롯이 모두 가득 찼습니다.");
            return;
        }

        slots.Add(newPlayer);
        UnityEngine.Debug.Log($"캐릭터 {newPlayer.NickName} 추가 완료!");
    }

    public List<PlayerData> GetAllSlotData()
    {
        return slots;
    }

    public void SaveSlotsData()
    {
        string jsonData = UnityEngine.JsonUtility.ToJson(new SerializationWrapper<PlayerData>(slots), true);
        UnityEngine.PlayerPrefs.SetString("SlotData", jsonData);
        UnityEngine.PlayerPrefs.Save();
        UnityEngine.Debug.Log("슬롯 데이터 저장 완료!");
    }

    public void LoadSlotsData()
    {
        if (UnityEngine.PlayerPrefs.HasKey("SlotData"))
        {
            string jsonData = UnityEngine.PlayerPrefs.GetString("SlotData");
            var loadedData = UnityEngine.JsonUtility.FromJson<SerializationWrapper<PlayerData>>(jsonData);
            slots = loadedData.Items;
            UnityEngine.Debug.Log("슬롯 데이터 로드 완료!");
        }
        else
        {
            UnityEngine.Debug.Log("저장된 슬롯 데이터가 없습니다.");
        }
    }
}
