using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList;

    public CharacterList(IPlayerRepository repository)
    {
        if (repository == null)
        {
            Debug.LogError("IPlayerRepository가 초기화되지 않았습니다.");
            _characterList = new List<PlayerData>(); // 기본 리스트 초기화
            return;
        }

        _characterList = repository.LoadAllPlayerData(); // 모든 캐릭터 데이터 로드
    }

    public bool AddCharacter(PlayerData newCharacter)
    {
        if (_characterList.Count >= 4) // 최대 4개의 슬롯
        {
            Debug.LogWarning("슬롯이 가득 찼습니다.");
            return false;
        }

        _characterList.Add(newCharacter);
        SaveListsData(); // 추가 후 데이터 저장
        return true;
    }

    public List<PlayerData> GetAllCharacters()
    {
        return new List<PlayerData>(_characterList); // 리스트 복사 반환
    }

    public void SaveListsData()
    {
        GameManager.Instance.DataManager.Repository.SaveAllPlayerData(_characterList); // 모든 캐릭터 데이터 저장
        Debug.Log("캐릭터 리스트 저장 완료");
    }

    public void LoadAllCharacters()
    {
        _characterList = GameManager.Instance.DataManager.Repository.LoadAllPlayerData();
        Debug.Log("캐릭터 리스트 로드 완료");
    }

    public PlayerData GetCharacter(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _characterList.Count)
        {
            Debug.LogWarning("잘못된 슬롯 인덱스입니다.");
            return null;
        }

        return _characterList[slotIndex];
    }

    public void RemoveCharacter(PlayerData characterToRemove)
    {
        if (_characterList.Contains(characterToRemove))
        {
            _characterList.Remove(characterToRemove); // 리스트에서 캐릭터 제거
            SaveListsData(); // 삭제 후 데이터 저장
            Debug.Log($"캐릭터 {characterToRemove.NickName} 삭제 완료");
        }
        else
        {
            Debug.LogWarning("삭제하려는 캐릭터가 리스트에 존재하지 않습니다.");
        }
    }
}
