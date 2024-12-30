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

    private void OnEnable()
    {
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
        var characters = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();

        Debug.Log($"�ε�� ĳ���� ��: {characters.Count}");
        foreach (var character in characters)
        {
            Debug.Log($"�ε�� ĳ����: {character.NickName}, HP: {character.CurrentHP}");
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

            // ����� �α� �߰�
            Debug.Log($"���� ���� �Ϸ�: {playerData.NickName}, HP: {playerData.CurrentHP}");
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
        SetButtonsInteractable(true); // ��ư Ȱ��ȭ

        // ���õ� ĳ���� �����͸� JSON���� ���
        string characterJson = JsonUtility.ToJson(_selectedCharacter, true);
        Debug.Log($"ĳ���� ���õ�: {_selectedCharacter.NickName}\n������: {characterJson}");
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

        GameManager.Instance.DataManager.CharacterList.RemoveCharacter(_selectedCharacter); // ĳ���� ����
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
