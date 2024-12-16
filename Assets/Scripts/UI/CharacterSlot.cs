using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public PlayerData SlotData; // ������ ������
    [SerializeField] private TextMeshProUGUI nicknameText; // �г��� ǥ��
    [SerializeField] private Button selectButton; // ���� ���� ��ư

    // ���� �ʱ�ȭ
    public void InitializeSlot(PlayerData data)
    {
        SlotData = data;

        if (SlotData != null)
        {
            nicknameText.text = SlotData.NickName;
        }
        else
        {
            nicknameText.text = "Empty Slot";
        }
    }

    // ���� ���� �� ȣ��
    public void OnSlotSelected()
    {
        if (SlotData == null)
        {
            Debug.LogWarning("�� �����Դϴ�.");
        }
        else
        {
            Debug.Log($"{SlotData.NickName} ĳ���Ͱ� ���õǾ����ϴ�!");
            // ���� �ε� ó�� �� �߰� ���
        }
    }
}
