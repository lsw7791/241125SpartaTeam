using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> slots = new List<PlayerData>();
    private IPlayerRepository repository;

    public CharacterList(IPlayerRepository repository)
    {
        this.repository = repository;
        LoadSlotsData();
    }

    // ���ο� ĳ���� �߰�
    public void AddCharacter(PlayerData newPlayer)
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

    // ��� ���� ������ ��ȯ
    public List<PlayerData> GetAllSlots()
    {
        return slots;
    }

    // ������ ����
    public void SaveSlotsData()
    {
        repository.SavePlayerData(slots);
        Debug.Log("���� ������ ���� �Ϸ�!");
    }

    // ������ �ε�
    public void LoadSlotsData()
    {
        slots = repository.LoadPlayerData();
        Debug.Log("���� ������ �ε� �Ϸ�!");
    }

    // Ư�� ���Կ��� ĳ���� ������ ��ȯ
    public PlayerData GetCharacter(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count)
        {
            Debug.LogWarning("�߸��� ���� �ε����Դϴ�.");
            return null;
        }

        return slots[slotIndex];
    }
}
