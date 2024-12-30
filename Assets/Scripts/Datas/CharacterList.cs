using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList;
    private readonly string _listFilePath;

    public CharacterList()
    {
        _listFilePath = Path.Combine(Application.persistentDataPath, "CharacterList.json");

        if (File.Exists(_listFilePath))
        {
            var json = File.ReadAllText(_listFilePath);
            _characterList = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json)?.List ?? new List<PlayerData>();
        }
        else
        {
            _characterList = new List<PlayerData>();
            SaveCharacterList();
        }
    }

    public bool AddCharacter(PlayerData newCharacter)
    {
        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("������ ���� á���ϴ�.");
            return false;
        }
        Debug.Log(_characterList);

        _characterList.Add(newCharacter);
        SaveCharacterList();
        return true;
    }

    public List<PlayerData> GetAllCharacters()
    {
        return new List<PlayerData>(_characterList);
    }

    public void RemoveCharacter(PlayerData characterToRemove)
    {
        if (_characterList.Contains(characterToRemove))
        {
            _characterList.Remove(characterToRemove);
            SaveCharacterList();
            Debug.Log($"ĳ���� {characterToRemove.NickName} ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("�����Ϸ��� ĳ���Ͱ� ����Ʈ�� �������� �ʽ��ϴ�.");
        }
    }

    private void SaveCharacterList()
    {
        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(_characterList));
        File.WriteAllText(_listFilePath, json);
        Debug.Log("ĳ���� ����Ʈ ���� �Ϸ�");
    }
}

[System.Serializable]
public class SerializableListWrapper<T>
{
    public List<T> List;

    public SerializableListWrapper(List<T> list)
    {
        List = list;
    }
}
