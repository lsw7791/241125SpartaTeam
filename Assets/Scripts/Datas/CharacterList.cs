using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList;

    public CharacterList(IPlayerRepository repository)
    {
        _characterList = GameManager.Instance.DataManager.Repository.LoadAllPlayerData();  // ��� ĳ���� ������ �ε�
    }

    public bool AddCharacter(PlayerData newCharacter)
    {
        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("������ ���� á���ϴ�.");
            return false;
        }

        _characterList.Add(newCharacter);
        SaveListsData();  // �߰� �� ������ ����
        return true;
    }

    public List<PlayerData> GetAllCharacters()
    {
        return _characterList;
    }

    public void SaveListsData()
    {
        GameManager.Instance.DataManager.Repository.SaveAllPlayerData(_characterList);  // ��� ĳ���� ������ ����
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
}
