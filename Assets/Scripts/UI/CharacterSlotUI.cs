using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] private Transform slotParent;  // ���� �θ� ������Ʈ
    [SerializeField] private GameObject slotPrefab;  // ���� UI ������
    private CharacterList _characterList;

    void Start()
    {
        _characterList = new CharacterList(new FilePlayerRepository());
        slotPrefab = Resources.Load<GameObject>("Prefabs/UI/CharacterSlot");
        LoadSlots();  // ���� UI ����
    }

    public void LoadSlots()
    {
        // ���� ���� UI ����
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // ĳ���� ���� ������ ��������
        var slots = _characterList.GetAllSlots();

        // �� ���Կ� �����͸� ǥ��
        foreach (var playerData in slots)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
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
