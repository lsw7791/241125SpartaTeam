using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList;

    public CharacterList(IPlayerRepository repository)
    {
        if (repository == null)
        {
            Debug.LogError("IPlayerRepository�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            _characterList = new List<PlayerData>(); // �⺻ ����Ʈ �ʱ�ȭ
            return;
        }

        _characterList = repository.LoadAllPlayerData(); // ��� ĳ���� ������ �ε�
    }

    public bool AddCharacter(PlayerData newCharacter)
    {
        if (_characterList.Count >= 4) // �ִ� 4���� ����
        {
            Debug.LogWarning("������ ���� á���ϴ�.");
            return false;
        }

        _characterList.Add(newCharacter);
        SaveListsData(); // �߰� �� ������ ����
        return true;
    }

    public List<PlayerData> GetAllCharacters()
    {
        return new List<PlayerData>(_characterList); // ����Ʈ ���� ��ȯ
    }

    public void SaveListsData()
    {
        GameManager.Instance.DataManager.Repository.SaveAllPlayerData(_characterList); // ��� ĳ���� ������ ����
        Debug.Log("ĳ���� ����Ʈ ���� �Ϸ�");
    }

    public void LoadAllCharacters()
    {
        _characterList = GameManager.Instance.DataManager.Repository.LoadAllPlayerData();
        Debug.Log("ĳ���� ����Ʈ �ε� �Ϸ�");
    }

    public PlayerData GetCharacter(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _characterList.Count)
        {
            Debug.LogWarning("�߸��� ���� �ε����Դϴ�.");
            return null;
        }

        return _characterList[slotIndex];
    }

    public void RemoveCharacter(PlayerData characterToRemove)
    {
        if (_characterList.Contains(characterToRemove))
        {
            _characterList.Remove(characterToRemove); // ����Ʈ���� ĳ���� ����
            SaveListsData(); // ���� �� ������ ����
            Debug.Log($"ĳ���� {characterToRemove.NickName} ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("�����Ϸ��� ĳ���Ͱ� ����Ʈ�� �������� �ʽ��ϴ�.");
        }
    }
}
