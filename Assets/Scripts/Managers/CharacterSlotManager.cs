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
            UnityEngine.Debug.LogWarning("������ ��� ���� á���ϴ�.");
            return;
        }

        slots.Add(newPlayer);
        UnityEngine.Debug.Log($"ĳ���� {newPlayer.NickName} �߰� �Ϸ�!");
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
        UnityEngine.Debug.Log("���� ������ ���� �Ϸ�!");
    }

    public void LoadSlotsData()
    {
        if (UnityEngine.PlayerPrefs.HasKey("SlotData"))
        {
            string jsonData = UnityEngine.PlayerPrefs.GetString("SlotData");
            var loadedData = UnityEngine.JsonUtility.FromJson<SerializationWrapper<PlayerData>>(jsonData);
            slots = loadedData.Items;
            UnityEngine.Debug.Log("���� ������ �ε� �Ϸ�!");
        }
        else
        {
            UnityEngine.Debug.Log("����� ���� �����Ͱ� �����ϴ�.");
        }
    }
}
