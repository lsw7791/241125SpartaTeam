using System.Collections.Generic;
using UnityEngine;

public class CharacterList
{
    private List<PlayerData> _characterList = new List<PlayerData>();

    public CharacterList()
    {
        LoadListsData();  // 초기화 시 데이터 로드
    }

    // 캐릭터 추가
    public bool AddCharacter(PlayerData newPlayer)
    {
        // 캐릭터가 추가될 때마다 _characterList의 개수를 출력하여 디버깅
        Debug.Log($"현재 캐릭터 개수: {_characterList.Count}");

        if (_characterList.Count >= 4)
        {
            Debug.LogWarning("슬롯이 모두 가득 찼습니다.");
            return false;
        }

        _characterList.Add(newPlayer);
        Debug.Log($"캐릭터 {newPlayer.NickName} 추가 완료!");
        return true;
    }


    // 모든 캐릭터 데이터 반환
    public List<PlayerData> GetAllLists()
    {
        return _characterList;
    }

    // 특정 캐릭터 반환
    public PlayerData GetCharacter(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _characterList.Count)
        {
            Debug.LogWarning("잘못된 슬롯 인덱스입니다.");
            return null;
        }

        return _characterList[slotIndex];
    }

    // 캐릭터 초기화 (외부에서 로드한 데이터를 대체)
    public void SetCharacters(List<PlayerData> players)
    {
        _characterList = new List<PlayerData>(players);
        Debug.Log($"캐릭터 리스트 초기화 완료: {_characterList.Count}명 로드됨");
    }

    // 데이터 저장
    public void SaveListsData()
    {
        var repository = GameManager.Instance.Repository;
        if (repository == null)
        {
            Debug.LogError("저장소가 초기화되지 않았습니다.");
            return;
        }

        repository.SaveAllPlayerData(_characterList);  // PlayerData 리스트를 저장
        Debug.Log("캐릭터 데이터 저장 완료!");
    }

    // 데이터 로드
    public void LoadListsData()
    {
        var repository = GameManager.Instance.Repository;
        if (repository == null)
        {
            Debug.LogError("저장소가 초기화되지 않았습니다.");
            return;
        }

        _characterList = repository.LoadAllPlayerData();  // PlayerData 리스트를 로드
        Debug.Log($"캐릭터 데이터 로드 완료! {_characterList.Count}명의 캐릭터를 불러왔습니다.");
    }
}
