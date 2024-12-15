[System.Serializable]
public class CharacterSlot
{
    public string SlotName; // ���� �̸� (��: "���� 1", "���� 2" ��)
    public PlayerData playerData; // �ش� ���Կ� ����� ĳ���� ������

    public CharacterSlot(string slotName)
    {
        SlotName = slotName;
        playerData = new PlayerData(); // ���ο� ĳ���� �����͸� �ʱ�ȭ
    }

    // ���� �����͸� ����
    public void SaveSlotData(Player player, string fileName)
    {
        SaveLoadManager.SavePlayerData(player, fileName);
    }

    // ���� ������ �ε�
    public void LoadSlotData(Player player, string fileName)
    {
        SaveLoadManager.LoadPlayerData(player, fileName);
    }
}
