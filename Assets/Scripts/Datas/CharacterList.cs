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
            Debug.LogWarning("슬롯이 가득 찼습니다.");
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
            Debug.Log($"캐릭터 {characterToRemove.NickName} 삭제 완료");
        }
        else
        {
            Debug.LogWarning("삭제하려는 캐릭터가 리스트에 존재하지 않습니다.");
        }
    }

    private void SaveCharacterList()
    {
        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(_characterList));
        File.WriteAllText(_listFilePath, json);
        Debug.Log("캐릭터 리스트 저장 완료");
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
