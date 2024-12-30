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
            Debug.Log($"로드된 JSON 데이터: {json}");
            _characterList = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json)?.List ?? new List<PlayerData>();
        }
        else
        {
            Debug.LogWarning("CharacterList.json 파일이 존재하지 않습니다. 새로 생성합니다.");
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

        Debug.Log($"캐릭터 추가 전 리스트: {_characterList.Count}개");
        Debug.Log($"추가할 캐릭터: {JsonUtility.ToJson(newCharacter, true)}");

        _characterList.Add(newCharacter);
        SaveCharacterList();

        Debug.Log($"캐릭터 추가 후 리스트: {_characterList.Count}개");
        return true;
    }

    public List<PlayerData> GetAllCharacters()
    {
        Debug.Log($"전체 캐릭터 수: {_characterList.Count}");
        foreach (var character in _characterList)
        {
            Debug.Log($"캐릭터 데이터: {JsonUtility.ToJson(character, true)}");
        }
        return new List<PlayerData>(_characterList);
    }

    public void RemoveCharacter(PlayerData characterToRemove)
    {
        if (_characterList.Contains(characterToRemove))
        {
            Debug.Log($"삭제할 캐릭터: {JsonUtility.ToJson(characterToRemove, true)}");
            _characterList.Remove(characterToRemove);
            SaveCharacterList();
            Debug.Log($"캐릭터 {characterToRemove.NickName} 삭제 완료. 남은 캐릭터 수: {_characterList.Count}");
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
        Debug.Log($"캐릭터 리스트 저장 완료: {json}");
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
