using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotManager : MonoBehaviour
{
    public List<CharacterSlot> characterSlots; // 4개의 캐릭터 슬롯
    public Player player; // 현재 플레이어 캐릭터
    public string repositoryPrefix = "CharacterSlot_"; // 각 슬롯에 대해 저장되는 파일 이름에 접두사 추가

    private void Start()
    {
        // 슬롯 초기화 (4개의 슬롯을 초기화)
        characterSlots = new List<CharacterSlot>
        {
            new CharacterSlot("Slot 1"),
            new CharacterSlot("Slot 2"),
            new CharacterSlot("Slot 3"),
            new CharacterSlot("Slot 4")
        };

        // 첫 번째 캐릭터 자동 저장
        SaveCharacterData();
    }

    // 슬롯 번호를 받아 해당 슬롯의 데이터를 로드
    public void LoadCharacterData(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < characterSlots.Count)
        {
            string fileName = repositoryPrefix + slotIndex; // 예: CharacterSlot_0.json
            characterSlots[slotIndex].LoadSlotData(player, fileName);
            Debug.Log($"슬롯 {slotIndex + 1} 데이터 로드 완료");
        }
        else
        {
            Debug.LogError("잘못된 슬롯 인덱스");
        }
    }

    // 빈 슬롯을 찾아 캐릭터를 저장
    public void SaveCharacterData()
    {
        int availableSlotIndex = GetAvailableSlotIndex(); // 사용되지 않은 첫 번째 슬롯을 찾음
        if (availableSlotIndex >= 0)
        {
            string fileName = repositoryPrefix + availableSlotIndex; // 예: CharacterSlot_0.json
            characterSlots[availableSlotIndex].SaveSlotData(player, fileName);
            Debug.Log($"슬롯 {availableSlotIndex + 1} 데이터 저장 완료");
        }
        else
        {
            Debug.LogWarning("모든 슬롯이 꽉 찼습니다.");
        }
    }

    // 사용되지 않은 첫 번째 슬롯을 찾는 메서드
    private int GetAvailableSlotIndex()
    {
        for (int i = 0; i < characterSlots.Count; i++)
        {
            // 만약 슬롯에 캐릭터 데이터가 없다면 그 슬롯을 반환
            if (characterSlots[i].playerData == null)
            {
                return i;
            }
        }
        return -1; // 모든 슬롯이 꽉 차면 -1 반환
    }
}
