using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform slotParent;  // 슬롯 부모 오브젝트
    [SerializeField] private GameObject slotPrefab;  // 슬롯 UI 프리팹
    private CharacterList _characterList;

    void Start()
    {
        _characterList = new CharacterList(new FilePlayerRepository());
        slotPrefab = Resources.Load<GameObject>("Prefabs/UI/CharacterSlot");
        LoadSlots();  // 슬롯 UI 갱신
    }

    public void LoadSlots()
    {
        // 기존 슬롯 UI 제거
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // 캐릭터 슬롯 데이터 가져오기
        var slots = _characterList.GetAllSlots();

        // 각 슬롯에 데이터를 표시
        foreach (var playerData in slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            TextMeshProUGUI slotText = newSlot.GetComponentInChildren<TextMeshProUGUI>();

            if (slotText != null)
            {
                slotText.text = playerData.NickName;  // 캐릭터 이름 표시
            }

            // 슬롯을 클릭했을 때 해당 캐릭터 선택
            newSlot.GetComponent<Button>().onClick.AddListener(() => OnSlotSelected(playerData));
        }
    }

    // 슬롯 선택 시 해당 캐릭터로 게임 시작
    private void OnSlotSelected(PlayerData selectedCharacter)
    {
        Debug.Log($"캐릭터 {selectedCharacter.NickName} 선택됨!");
        // 게임 시작 로직 (예: 게임 매니저로 캐릭터 데이터 전달)
    }
}
