using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList = new List<PlayerData>();

    public CharacterList(IPlayerRepository repository)
    {
        GameManager.Instance.Repository = repository;
        LoadListsData();  // ����ҿ��� ������ �ε�
    }

    // ���ο� ĳ���� �߰�
    public void AddCharacter(PlayerData newPlayer)
    {
        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("������ ��� ���� á���ϴ�.");
            return;
        }

        _characterList.Add(newPlayer);  // �޸𸮿� �߰�
        SaveListsData();  // ����ҿ� �ݿ�
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
        GameManager.Instance.Repository.SavePlayerData(_characterList);  // ����ҿ� ����
        Debug.Log("ĳ���� ������ ���� �Ϸ�!");
    }

    // ������ �ε�
    public void LoadListsData()
    {
        _characterList = GameManager.Instance.Repository.LoadPlayerData();  // ����ҿ��� �ҷ�����
        Debug.Log("ĳ���� ������ �ε� �Ϸ�!");
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
