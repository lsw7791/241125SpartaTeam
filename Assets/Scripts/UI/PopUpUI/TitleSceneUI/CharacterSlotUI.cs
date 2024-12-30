using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform _slotParent;  // ���� �θ� ������Ʈ
    [SerializeField] private GameObject _slotPrefab; // ���� ������
    [SerializeField] private Button _executeButton;  // ���� ��ư
    [SerializeField] private Button _deleteButton;   // ���� ��ư
    [SerializeField] private Button _backButton;     // �ڷΰ��� ��ư

    private PlayerData _selectedCharacter;          // ���õ� ĳ���� ������
    private CharacterList _characterList;           // ĳ���� ����Ʈ ����

    private void OnEnable()
    {
        _characterList = GameManager.Instance.DataManager.CharacterList; // ĳ���� ����Ʈ �ʱ�ȭ
        _characterList.LoadAllCharacters();
        LoadSlots(); // ���� �ε�

        // ��ư ������ ���
        _executeButton.onClick.AddListener(OnExecuteButtonClicked);
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);

        SetButtonsInteractable(false); // ��ư �ʱ� ���� ��Ȱ��ȭ
    }

    private void LoadSlots()
    {
        // ���� ���� ����
        ClearSlots();

        // ĳ���� ������ �ε� �� ���� ����
        var characters = _characterList.GetAllCharacters();
        foreach (var character in characters)
        {
            CreateSlot(character);
        }
    }

    private void CreateSlot(PlayerData playerData)
    {
        // ���� ������ ����
        GameObject newSlot = Instantiate(_slotPrefab, _slotParent);
        var slotComponent = newSlot.GetComponent<CharacterSlot>();

        if (slotComponent != null)
        {
            // ���� �ʱ�ȭ
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
        SetButtonsInteractable(true); // ��ư Ȱ��ȭ
        Debug.Log($"ĳ���� {_selectedCharacter.NickName} ���õ�.");
    }

    private void OnExecuteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("���õ� ĳ���Ͱ� �����ϴ�.");
            return;
        }

        UIManager.Instance.CloseUI<CharacterSlotUI>();
        GameManager.Instance.StartGame(_selectedCharacter); // ���� ����
    }

    private void OnDeleteButtonClicked()
    {
        if (_selectedCharacter == null)
        {
            Debug.LogWarning("������ ĳ���Ͱ� �����ϴ�.");
            return;
        }

        _characterList.RemoveCharacter(_selectedCharacter); // ĳ���� ����
        _selectedCharacter = null; // ���� �ʱ�ȭ
        LoadSlots(); // ���� UI ����
        SetButtonsInteractable(false); // ��ư ��Ȱ��ȭ
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
