using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform slotParent;  // 슬롯 부모 오브젝트
    [SerializeField] private GameObject slotPrefab;  // 슬롯 UI 프리팹
    [SerializeField] private Button _executeButton;  // 실행 버튼
    [SerializeField] private Button _deleteButton;   // 삭제 버튼
    [SerializeField] private Button _backButton;     // 뒤로가기 버튼

    private PlayerData selectedCharacter;  // 현재 선택된 캐릭터 데이터

    private void OnEnable()
    {
        LoadSlots();  // 슬롯 UI 갱신
        _backButton.onClick.AddListener(OnBackButtonClicked);  // 뒤로가기 버튼 이벤트

        // 버튼 초기화
        _executeButton.onClick.AddListener(OnExecuteButtonClicked);  // 실행 버튼
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);    // 삭제 버튼

        // 실행과 삭제 버튼 비활성화
        SetButtonsInteractable(false);
    }

    private void LoadSlots()
    {
        // 기존 슬롯 UI 제거
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // 캐릭터 슬롯 데이터 가져오기
        var slots = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();

        // 각 슬롯에 데이터를 표시
        foreach (var playerData in slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            TextMeshProUGUI slotText = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            Button slotButton = newSlot.GetComponent<Button>();

            if (slotText != null)
            {
                slotText.text = playerData.NickName;  // 캐릭터 이름 표시
            }

            // 슬롯 클릭 시 캐릭터 선택
            slotButton.onClick.AddListener(() => OnSlotSelected(playerData));
        }
    }

    // 슬롯 클릭 시, 해당 캐릭터로 선택
    private void OnSlotSelected(PlayerData playerData)
    {
        selectedCharacter = playerData;  // 선택된 캐릭터 데이터 저장
        SetButtonsInteractable(true);     // 실행과 삭제 버튼 활성화
        Debug.Log($"캐릭터 {playerData.NickName} 선택됨!");
    }

    // 실행 버튼 클릭 시, 해당 캐릭터로 게임 시작
    private void OnExecuteButtonClicked()
    {
        Debug.Log($"게임 시작: {selectedCharacter.NickName}");
        GameManager.Instance.StartGame(selectedCharacter);  // 게임 시작
    }

    // 삭제 버튼 클릭 시, 해당 캐릭터 삭제 및 슬롯 UI 제거
    private void OnDeleteButtonClicked()
    {
        GameManager.Instance.DataManager.CharacterList.RemoveCharacter(selectedCharacter);  // 캐릭터 삭제
        selectedCharacter = null;  // 선택된 캐릭터 초기화
        LoadSlots();  // 슬롯 UI 갱신
        SetButtonsInteractable(false);  // 버튼 비활성화
    }

    // 뒤로가기 버튼 클릭 시, 슬롯 UI 닫기
    private void OnBackButtonClicked()
    {
        UIManager.Instance.ToggleUI<CharacterSlotUI>();  // 뒤로가기 시 UI 닫기
        Debug.Log("뒤로가기 클릭됨!");
    }

    // 실행과 삭제 버튼 활성화/비활성화 설정
    private void SetButtonsInteractable(bool isInteractable)
    {
        _executeButton.interactable = isInteractable;
        _deleteButton.interactable = isInteractable;
    }
}
