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

    private PlayerData selectedCharacter;  // ���õ� ĳ���� ������

    private void OnEnable()
    {
        LoadSlots();

        _executeButton.onClick.AddListener(OnExecuteButtonClicked);
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
        _backButton.onClick.AddListener(OnBackButtonClicked);

        SetButtonsInteractable(false); // ��ư ��Ȱ��ȭ �ʱ�ȭ
    }

    private void LoadSlots()
    {
        foreach (Transform child in _slotParent)
        {
            Destroy(child.gameObject);
        }

        // ĳ���� ������ �ҷ�����
        var characterList = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();

        foreach (var playerData in characterList)
        {
            GameObject newSlot = Instantiate(_slotPrefab, _slotParent);
            var slotComponent = newSlot.GetComponent<CharacterSlot>();

            if (slotComponent != null)
            {
                // ���� �ʱ�ȭ
                slotComponent.InitializeSlot(playerData, OnSlotSelected);
            }
        }
    }

    private void OnSlotSelected(PlayerData playerData)
    {
        selectedCharacter = playerData;
        SetButtonsInteractable(true);
        Debug.Log($"ĳ���� {playerData.NickName} ���õ�.");
    }

    private void OnExecuteButtonClicked()
    {
        if (selectedCharacter != null)
        {
            Debug.Log($"���� ����: {selectedCharacter.NickName}");
            GameManager.Instance.StartGame(selectedCharacter);
        }
    }

    private void OnDeleteButtonClicked()
    {
        if (selectedCharacter != null)
        {
            GameManager.Instance.DataManager.CharacterList.RemoveCharacter(selectedCharacter);
            selectedCharacter = null; // ���� �ʱ�ȭ
            LoadSlots(); // ���� UI ����
            SetButtonsInteractable(false); // ��ư ��Ȱ��ȭ
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
