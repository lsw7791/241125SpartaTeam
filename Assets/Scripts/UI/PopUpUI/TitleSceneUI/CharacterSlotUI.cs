using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform _slotParent;  // 슬롯 부모 오브젝트
    [SerializeField] private GameObject _slotPrefab; // 슬롯 프리팹
    [SerializeField] private Button _executeButton;  // 실행 버튼
    [SerializeField] private Button _deleteButton;   // 삭제 버튼
    [SerializeField] private Button _backButton;     // 뒤로가기 버튼

    private PlayerData _selectedCharacter;          // 선택된 캐릭터 데이터
    private CharacterList _characterList;           // 캐릭터 리스트 참조

    private void OnEnable()
    {
        _characterList = GameManager.Instance.DataManager.CharacterList; // 캐릭터 리스트 초기화
        _characterList.LoadAllCharacters();
        LoadSlots(); // 슬롯 로드

        // 버튼 리스너 등록
        _executeButton.onClick.AddListener(OnExecuteButtonClicked);
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);

        SetButtonsInteractable(false); // 버튼 초기 상태 비활성화
    }

    private void LoadSlots()
    {
        // 기존 슬롯 제거
        ClearSlots();

        // 캐릭터 데이터 로드 및 슬롯 생성
        var characters = _characterList.GetAllCharacters();
        foreach (var character in characters)
        {
            CreateSlot(character);
        }
    }

    private void CreateSlot(PlayerData playerData)
    {
        // 슬롯 프리팹 생성
        GameObject newSlot = Instantiate(_slotPrefab, _slotParent);
        var slotComponent = newSlot.GetComponent<CharacterSlot>();

        if (slotComponent != null)
        {
            // 슬롯 초기화
            slotComponent.InitializeSlot(playerData, OnSlotSelected);
        }
    }

    private void ClearSlots()
    {
        foreach (Transform child in _slotParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnSlotSelected(PlayerData playerData)
    {
        _selectedCharacter = playerData;
        SetButtonsInteractable(true); // 버튼 활성화
        Debug.Log($"캐릭터 {_selectedCharacter.NickName} 선택됨.");
    }

    private void OnExecuteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("선택된 캐릭터가 없습니다.");
            return;
        }

        UIManager.Instance.CloseUI<CharacterSlotUI>();
        GameManager.Instance.StartGame(_selectedCharacter); // 게임 시작
    }

    private void OnDeleteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("삭제할 캐릭터가 없습니다.");
            return;
        }

        _characterList.RemoveCharacter(_selectedCharacter); // 캐릭터 삭제
        _selectedCharacter = null; // 선택 초기화
        LoadSlots(); // 슬롯 UI 갱신
        SetButtonsInteractable(false); // 버튼 비활성화
    }

    private void OnBackButtonClicked()
    {
        UIManager.Instance.CloseUI<CharacterSlotUI>();
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        _executeButton.interactable = isInteractable;
        _deleteButton.interactable = isInteractable;
    }
}
