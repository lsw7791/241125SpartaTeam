//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class CharacterSlotUIManager : MonoBehaviour
//{
//    public GameObject slotPrefab;  // 슬롯 UI 프리팹
//    public Transform slotParent;  // 슬롯 부모
//    private List<GameObject> slotObjects = new List<GameObject>();  // 슬롯 객체 리스트

//    void Awake()
//    {
//        // slotParent가 에디터에서 할당되지 않았다면, 오류 처리
//        if (slotParent == null)
//        {
//            Debug.LogError("SlotParent가 할당되지 않았습니다! 게임 오브젝트에서 확인하세요.");
//            return;
//        }

//        LoadSlots();  // 슬롯 로딩
//    }

//    public void LoadSlots()
//    {
//        if (GameManager.Instance == null)
//        {
//            Debug.LogError("GameManager가 초기화되지 않았습니다!");
//            return;
//        }

//        List<PlayerData> loadedCharacters = GameManager.Instance.GetAllPlayerData();  // 데이터 로드

//        // 기존 슬롯 UI 제거
//        foreach (Transform child in slotParent)
//        {
//            Destroy(child.gameObject);
//        }

//        slotObjects.Clear();  // 슬롯 객체 리스트 초기화

//        // 새로운 슬롯 UI 생성
//        foreach (var character in loadedCharacters)
//        {
//            GameObject newSlot = Instantiate(slotPrefab, slotParent);
//            var slotComponent = newSlot.GetComponent<CharacterSlot>();
//            slotComponent.InitializeSlot(character, OnSlotSelected);

//            slotObjects.Add(newSlot);  // 슬롯 객체 리스트에 추가
//        }
//    }

//    // 슬롯 선택 시 처리
//    private void OnSlotSelected(PlayerData selectedCharacter)
//    {
//        Debug.Log($"캐릭터 {selectedCharacter.NickName} 선택됨!");
//        GameManager.Instance.StartGame(selectedCharacter);
//    }

//    // UI 업데이트 메서드
//    public void UpdateSlotUI(List<PlayerData> playerDataList)
//    {
//        // 기존 슬롯 UI 제거
//        foreach (var slotObject in slotObjects)
//        {
//            Destroy(slotObject);
//        }
//        slotObjects.Clear();

//        // 새로운 슬롯 UI 생성
//        foreach (var playerData in playerDataList)
//        {
//            GameObject newSlot = Instantiate(slotPrefab, slotParent);
//            // TextMeshProUGUI로 변경
//            TextMeshProUGUI slotText = newSlot.GetComponentInChildren<TextMeshProUGUI>();

//            if (slotText != null)
//            {
//                slotText.text = playerData.NickName; // 캐릭터 이름 설정
//            }

//            slotObjects.Add(newSlot); // 슬롯 오브젝트 리스트에 추가
//        }
//    }
//}
