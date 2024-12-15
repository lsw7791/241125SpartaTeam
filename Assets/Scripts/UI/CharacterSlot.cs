[System.Serializable]
public class CharacterSlot
{
    public string SlotName; // 슬롯 이름 (예: "슬롯 1", "슬롯 2" 등)
    public PlayerData playerData; // 해당 슬롯에 저장된 캐릭터 데이터

    public CharacterSlot(string slotName)
    {
        SlotName = slotName;
        playerData = new PlayerData(); // 새로운 캐릭터 데이터를 초기화
    }

    // 슬롯 데이터를 저장
    public void SaveSlotData(Player player, string fileName)
    {
        SaveLoadManager.SavePlayerData(player, fileName);
    }

    // 슬롯 데이터 로드
    public void LoadSlotData(Player player, string fileName)
    {
        SaveLoadManager.LoadPlayerData(player, fileName);
    }
}
