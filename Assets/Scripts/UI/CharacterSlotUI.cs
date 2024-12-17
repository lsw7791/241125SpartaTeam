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

    private PlayerData selectedCharacter;  // 선택된 캐릭터 데이터

    private void OnEnable()
    {
        LoadSlots();

        _executeButton.onClick.AddListener(OnExecuteButtonClicked);
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);

        SetButtonsInteractable(false); // 버튼 비활성화 초기화
    }

    private void LoadSlots()
    {
        foreach (Transform child in _slotParent)
        {
            Destroy(child.gameObject);
        }

        // 캐릭터 데이터 불러오기
        var characterList = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();

        foreach (var playerData in characterList)
        {
            GameObject newSlot = Instantiate(_slotPrefab, _slotParent);
            var slotComponent = newSlot.GetComponent<CharacterSlot>();

            if (slotComponent != null)
            {
                // 슬롯 초기화
                slotComponent.InitializeSlot(playerData, OnSlotSelected);
            }
        }
    }

    private void OnSlotSelected(PlayerData playerData)
    {
        selectedCharacter = playerData;
        SetButtonsInteractable(true);
        Debug.Log($"캐릭터 {playerData.NickName} 선택됨.");
    }

    private void OnExecuteButtonClicked()
    {
        if (selectedCharacter != null)
        {
            Debug.Log($"게임 시작: {selectedCharacter.NickName}");
            GameManager.Instance.StartGame(selectedCharacter);
        }
    }

    private void OnDeleteButtonClicked()
    {
        if (selectedCharacter != null)
        {
            GameManager.Instance.DataManager.CharacterList.RemoveCharacter(selectedCharacter);
            selectedCharacter = null; // 선택 초기화
            LoadSlots(); // 슬롯 UI 갱신
            SetButtonsInteractable(false); // 버튼 비활성화
        }
    }

    private void OnBackButtonClicked()
    {
        UIManager.Instance.ToggleUI<CharacterSlotUI>();
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        _executeButton.interactable = isInteractable;
        _deleteButton.interactable = isInteractable;
    }
}
