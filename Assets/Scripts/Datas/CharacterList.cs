using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList = new List<PlayerData>();
    private IPlayerRepository repository;

    public CharacterList(IPlayerRepository repository)
    {
        this.repository = repository;
        LoadListsData();
    }

    // ���ο� ĳ���� �߰�
    public void AddCharacter(PlayerData newPlayer)
    {
        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("������ ��� ���� á���ϴ�.");
            return;
        }

        _characterList.Add(newPlayer);
        SaveListsData();
        Debug.Log($"ĳ���� {newPlayer.NickName} �߰� �Ϸ�!");
    }

    // ��� ĳ���� ������ ��ȯ
    public List<PlayerData> GetAllLists()
    {
        return _characterList;
    }

    // ������ ����
    public void SaveListsData()
    {
        repository.SavePlayerData(_characterList);
        Debug.Log("���� ������ ���� �Ϸ�!");
    }

    // ������ �ε�
    public void LoadListsData()
    {
        _characterList = repository.LoadPlayerData();
        Debug.Log("���� ������ �ε� �Ϸ�!");
    }

    // Ư�� ���Կ��� ĳ���� ������ ��ȯ
    public PlayerData GetCharacter(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _characterList.Count)
        {
            Debug.LogWarning("�߸��� ���� �ε����Դϴ�.");
            return null;
        }

        return _characterList[slotIndex];
    }
}
