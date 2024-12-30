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

    private void OnEnable()
    {
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
        var characters = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();

        Debug.Log($"로드된 캐릭터 수: {characters.Count}");
        foreach (var character in characters)
        {
            Debug.Log($"로드된 캐릭터: {character.NickName}, HP: {character.CurrentHP}");
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

            // 디버그 로그 추가
            Debug.Log($"슬롯 생성 완료: {playerData.NickName}, HP: {playerData.CurrentHP}");
        }
        else
        {
            Debug.LogWarning("CharacterSlot 컴포넌트를 찾을 수 없습니다.");
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

        // 선택된 캐릭터 데이터를 JSON으로 출력
        string characterJson = JsonUtility.ToJson(_selectedCharacter, true);
        Debug.Log($"캐릭터 선택됨: {_selectedCharacter.NickName}\n데이터: {characterJson}");
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

        GameManager.Instance.DataManager.CharacterList.RemoveCharacter(_selectedCharacter); // 캐릭터 삭제
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
