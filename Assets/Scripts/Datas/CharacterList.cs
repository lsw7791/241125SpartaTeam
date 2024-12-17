using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList;

    public CharacterList(IPlayerRepository repository)
    {
        _characterList = GameManager.Instance.DataManager.Repository.LoadAllPlayerData();  // 모든 캐릭터 데이터 로드
    }

    public bool AddCharacter(PlayerData newCharacter)
    {
        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("슬롯이 가득 찼습니다.");
            return false;
        }

        _characterList.Add(newCharacter);
        SaveListsData();  // 추가 후 데이터 저장
        return true;
    }

    public List<PlayerData> GetAllCharacters()
    {
        return _characterList;
    }

    public void SaveListsData()
    {
        GameManager.Instance.DataManager.Repository.SaveAllPlayerData(_characterList);  // 모든 캐릭터 데이터 저장
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
}
