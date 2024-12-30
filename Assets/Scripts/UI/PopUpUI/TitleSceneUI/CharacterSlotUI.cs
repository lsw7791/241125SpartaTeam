using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Button _executeButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _backButton;

    private PlayerData _selectedCharacter;

    private void OnEnable()
    {
        LoadSlots();

        _executeButton.onClick.AddListener(OnExecuteButtonClicked);
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);

        SetButtonsInteractable(false);
    }

    private void LoadSlots()
    {
        ClearSlots();

        var characters = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();
        Debug.Log($"로드된 캐릭터 수: {characters.Count}");

        foreach (var character in characters)
        {
            Debug.Log($"로드된 캐릭터: {JsonUtility.ToJson(character, true)}");
            CreateSlot(character);
        }
    }

    private void CreateSlot(PlayerData playerData)
    {
        GameObject newSlot = Instantiate(_slotPrefab, _slotParent);
        var slotComponent = newSlot.GetComponent<CharacterSlot>();

        if (slotComponent != null)
        {
            slotComponent.InitializeSlot(playerData, OnSlotSelected);
            Debug.Log($"슬롯 생성 완료: {playerData.NickName}");
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
        SetButtonsInteractable(true);

        // 선택된 캐릭터 데이터 출력
        string characterJson = JsonUtility.ToJson(_selectedCharacter, true);
        Debug.Log($"캐릭터 선택됨: {_selectedCharacter.NickName}\n데이터: {characterJson}");

        // 불러온 데이터를 Player 객체에 적용
        PlayerSaveLoad.ApplyPlayerData(GameManager.Instance.Player, _selectedCharacter);
    }


    private void OnExecuteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("선택된 캐릭터가 없습니다.");
            return;
        }

        Debug.Log($"게임 시작: {_selectedCharacter.NickName}");
        UIManager.Instance.CloseUI<CharacterSlotUI>();
        GameManager.Instance.StartGame(_selectedCharacter);
    }

    private void OnDeleteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("삭제할 캐릭터가 없습니다.");
            return;
        }

        Debug.Log($"캐릭터 삭제 시도: {_selectedCharacter.NickName}");
        GameManager.Instance.DataManager.CharacterList.RemoveCharacter(_selectedCharacter);
        _selectedCharacter = null;
        LoadSlots();
        SetButtonsInteractable(false);
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
