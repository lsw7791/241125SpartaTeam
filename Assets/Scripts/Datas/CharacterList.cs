using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList = new List<PlayerData>();

    public CharacterList()
    {
        LoadListsData();  // �ʱ�ȭ �� ������ �ε�
    }

    // ĳ���� �߰�
    public bool AddCharacter(PlayerData newPlayer)
    {
        // ĳ���Ͱ� �߰��� ������ _characterList�� ������ ����Ͽ� �����
        Debug.Log($"���� ĳ���� ����: {_characterList.Count}");

        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("������ ��� ���� á���ϴ�.");
            return false;
        }

        _characterList.Add(newPlayer);
        Debug.Log($"ĳ���� {newPlayer.NickName} �߰� �Ϸ�!");
        return true;
    }


    // ��� ĳ���� ������ ��ȯ
    public List<PlayerData> GetAllLists()
    {
        return _characterList;
    }

    // Ư�� ĳ���� ��ȯ
    public PlayerData GetCharacter(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _characterList.Count)
        {
            Debug.LogWarning("�߸��� ���� �ε����Դϴ�.");
            return null;
        }

        return _characterList[slotIndex];
    }

    // ĳ���� �ʱ�ȭ (�ܺο��� �ε��� �����͸� ��ü)
    public void SetCharacters(List<PlayerData> players)
    {
        _characterList = new List<PlayerData>(players);
        Debug.Log($"ĳ���� ����Ʈ �ʱ�ȭ �Ϸ�: {_characterList.Count}�� �ε��");
    }

    // ������ ����
    public void SaveListsData()
    {
        var repository = GameManager.Instance.Repository;
        if (repository == null)
        {
            Debug.LogError("����Ұ� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        repository.SaveAllPlayerData(_characterList);  // PlayerData ����Ʈ�� ����
        Debug.Log("ĳ���� ������ ���� �Ϸ�!");
    }

    // ������ �ε�
    public void LoadListsData()
    {
        var repository = GameManager.Instance.Repository;
        if (repository == null)
        {
            Debug.LogError("����Ұ� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        _characterList = repository.LoadAllPlayerData();  // PlayerData ����Ʈ�� �ε�
        Debug.Log($"ĳ���� ������ �ε� �Ϸ�! {_characterList.Count}���� ĳ���͸� �ҷ��Խ��ϴ�.");
    }
}
