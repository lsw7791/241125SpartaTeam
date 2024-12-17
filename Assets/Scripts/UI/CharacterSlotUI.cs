using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform slotParent;  // ���� �θ� ������Ʈ
    [SerializeField] private GameObject slotPrefab;  // ���� UI ������
    [SerializeField] private Button _executeButton;  // ���� ��ư
    [SerializeField] private Button _deleteButton;   // ���� ��ư
    [SerializeField] private Button _backButton;     // �ڷΰ��� ��ư

    private PlayerData selectedCharacter;  // ���� ���õ� ĳ���� ������

    private void OnEnable()
    {
        LoadSlots();  // ���� UI ����
        _backButton.onClick.AddListener(OnBackButtonClicked);  // �ڷΰ��� ��ư �̺�Ʈ

        // ��ư �ʱ�ȭ
        _executeButton.onClick.AddListener(OnExecuteButtonClicked);  // ���� ��ư
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);    // ���� ��ư

        // ����� ���� ��ư ��Ȱ��ȭ
        SetButtonsInteractable(false);
    }

    private void LoadSlots()
    {
        // ���� ���� UI ����
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // ĳ���� ���� ������ ��������
        var slots = GameManager.Instance.DataManager.CharacterList.GetAllCharacters();

        // �� ���Կ� �����͸� ǥ��
        foreach (var playerData in slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            TextMeshProUGUI slotText = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            Button slotButton = newSlot.GetComponent<Button>();

            if (slotText != null)
            {
                slotText.text = playerData.NickName;  // ĳ���� �̸� ǥ��
            }

            // ���� Ŭ�� �� ĳ���� ����
            slotButton.onClick.AddListener(() => OnSlotSelected(playerData));
        }
    }

    // ���� Ŭ�� ��, �ش� ĳ���ͷ� ����
    private void OnSlotSelected(PlayerData playerData)
    {
        selectedCharacter = playerData;  // ���õ� ĳ���� ������ ����
        SetButtonsInteractable(true);     // ����� ���� ��ư Ȱ��ȭ
        Debug.Log($"ĳ���� {playerData.NickName} ���õ�!");
    }

    // ���� ��ư Ŭ�� ��, �ش� ĳ���ͷ� ���� ����
    private void OnExecuteButtonClicked()
    {
        Debug.Log($"���� ����: {selectedCharacter.NickName}");
        GameManager.Instance.StartGame(selectedCharacter);  // ���� ����
    }

    // ���� ��ư Ŭ�� ��, �ش� ĳ���� ���� �� ���� UI ����
    private void OnDeleteButtonClicked()
    {
        GameManager.Instance.DataManager.CharacterList.RemoveCharacter(selectedCharacter);  // ĳ���� ����
        selectedCharacter = null;  // ���õ� ĳ���� �ʱ�ȭ
        LoadSlots();  // ���� UI ����
        SetButtonsInteractable(false);  // ��ư ��Ȱ��ȭ
    }

    // �ڷΰ��� ��ư Ŭ�� ��, ���� UI �ݱ�
    private void OnBackButtonClicked()
    {
        UIManager.Instance.ToggleUI<CharacterSlotUI>();  // �ڷΰ��� �� UI �ݱ�
        Debug.Log("�ڷΰ��� Ŭ����!");
    }

    // ����� ���� ��ư Ȱ��ȭ/��Ȱ��ȭ ����
    private void SetButtonsInteractable(bool isInteractable)
    {
        _executeButton.interactable = isInteractable;
        _deleteButton.interactable = isInteractable;
    }
}
