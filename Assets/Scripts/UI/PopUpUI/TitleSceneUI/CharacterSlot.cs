using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public PlayerData SlotData { get; private set; }  // ���Կ� ���� ������
    [SerializeField] private TextMeshProUGUI nicknameText;  // ĳ���� �̸��� ǥ���� �ؽ�Ʈ
    [SerializeField] private Button selectButton;  // ���� ���� ��ư

    // ���� �ʱ�ȭ �޼ҵ�
    public void InitializeSlot(PlayerData data, System.Action<PlayerData> onSelected)
    {
        // ������ �Ҵ�
        SlotData = data;

        // SlotData�� null�� ���� �ƴ� ��� ó��
        if (SlotData != null)
        {
            // ���Կ� �����Ͱ� ������ ĳ���� �̸��� ǥ��
            nicknameText.text = SlotData.NickName;
            selectButton.interactable = true;  // �����Ͱ� ������ ��ư Ȱ��ȭ
            Debug.Log($"���� �ʱ�ȭ �Ϸ�: {SlotData.NickName}");
        }
        else
        {
            // �����Ͱ� ������ "Empty Slot" ǥ��
            nicknameText.text = "Empty Slot";
            selectButton.interactable = false;  // �����Ͱ� ������ ��ư ��Ȱ��ȭ
            Debug.LogWarning("�� �����Դϴ�.");
        }

        // ��ư Ŭ�� �� OnSlotSelected ȣ��
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            if (SlotData != null)
            {
                onSelected?.Invoke(SlotData);  // ���õ� ĳ���� ������ ����
            }
            else
            {
                Debug.LogWarning("�� ������ ������ �� �����ϴ�.");
            }
        });
    }
}
