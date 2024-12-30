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
        Debug.Log($"�ε�� ĳ���� ��: {characters.Count}");

        foreach (var character in characters)
        {
            Debug.Log($"�ε�� ĳ����: {JsonUtility.ToJson(character, true)}");
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
            Debug.Log($"���� ���� �Ϸ�: {playerData.NickName}");
        }
        else
        {
            Debug.LogWarning("CharacterSlot ������Ʈ�� ã�� �� �����ϴ�.");
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

        // ���õ� ĳ���� ������ ���
        string characterJson = JsonUtility.ToJson(_selectedCharacter, true);
        Debug.Log($"ĳ���� ���õ�: {_selectedCharacter.NickName}\n������: {characterJson}");

        // �ҷ��� �����͸� Player ��ü�� ����
        PlayerSaveLoad.ApplyPlayerData(GameManager.Instance.Player, _selectedCharacter);
    }


    private void OnExecuteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("���õ� ĳ���Ͱ� �����ϴ�.");
            return;
        }

        Debug.Log($"���� ����: {_selectedCharacter.NickName}");
        UIManager.Instance.CloseUI<CharacterSlotUI>();
        GameManager.Instance.StartGame(_selectedCharacter);
    }

    private void OnDeleteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("������ ĳ���Ͱ� �����ϴ�.");
            return;
        }

        Debug.Log($"ĳ���� ���� �õ�: {_selectedCharacter.NickName}");
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
