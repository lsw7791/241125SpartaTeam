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

    // 새로운 캐릭터 추가
    public void AddCharacter(PlayerData newPlayer)
    {
        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("슬롯이 모두 가득 찼습니다.");
            return;
        }

        _characterList.Add(newPlayer);
        SaveListsData();
        Debug.Log($"캐릭터 {newPlayer.NickName} 추가 완료!");
    }

    // 모든 캐릭터 데이터 반환
    public List<PlayerData> GetAllLists()
    {
        return _characterList;
    }

    // 데이터 저장
    public void SaveListsData()
    {
        repository.SavePlayerData(_characterList);
        Debug.Log("슬롯 데이터 저장 완료!");
    }

    // 데이터 로드
    public void LoadListsData()
    {
        _characterList = repository.LoadPlayerData();
        Debug.Log("슬롯 데이터 로드 완료!");
    }

    // 특정 슬롯에서 캐릭터 데이터 반환
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
