using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform _slotParent;  // ���� �θ� ������Ʈ
    [SerializeField] private GameObject _slotPrefab;  // ���� UI ������

    void Start()
    {
        _slotPrefab = Resources.Load<GameObject>("Prefabs/UI/CharacterSlot");
        LoadSlots();  // ���� UI ����
    }

    public void LoadSlots()
    {
        // ���� ���� UI ����
        foreach (Transform child in _slotParent)
        {
            Destroy(child.gameObject);
        }

        // ĳ���� ���� ������ ��������
        var slots = GameManager.Instance.DataManager.CharacterList.GetAllLists();

        // �� ���Կ� �����͸� ǥ��
        foreach (var playerData in slots)
        {
            GameObject newSlot = Instantiate(_slotPrefab, _slotParent);
            TextMeshProUGUI slotText = newSlot.GetComponentInChildren<TextMeshProUGUI>();

            if (slotText != null)
            {
                slotText.text = playerData.NickName;  // ĳ���� �̸� ǥ��
            }

            // ������ Ŭ������ �� �ش� ĳ���� ����
            newSlot.GetComponent<Button>().onClick.AddListener(() => OnSlotSelected(playerData));
        }
    }

    // ���� ���� �� �ش� ĳ���ͷ� ���� ����
    private void OnSlotSelected(PlayerData selectedCharacter)
    {
        Debug.Log($"ĳ���� {selectedCharacter.NickName} ���õ�!");
        // ���� ���� ���� (��: ���� �Ŵ����� ĳ���� ������ ����)
    }

}
