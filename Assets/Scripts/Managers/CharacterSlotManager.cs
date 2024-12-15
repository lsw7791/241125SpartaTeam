using System.Collections.Generic;
using UnityEngine;

public class CharacterSlotManager : MonoBehaviour
{
    public List<CharacterSlot> characterSlots; // 4���� ĳ���� ����
    public Player player; // ���� �÷��̾� ĳ����
    public string repositoryPrefix = "CharacterSlot_"; // �� ���Կ� ���� ����Ǵ� ���� �̸��� ���λ� �߰�

    private void Start()
    {
        // ���� �ʱ�ȭ (4���� ������ �ʱ�ȭ)
        characterSlots = new List<CharacterSlot>
        {
            new CharacterSlot("Slot 1"),
            new CharacterSlot("Slot 2"),
            new CharacterSlot("Slot 3"),
            new CharacterSlot("Slot 4")
        };

        // ù ��° ĳ���� �ڵ� ����
        SaveCharacterData();
    }

    // ���� ��ȣ�� �޾� �ش� ������ �����͸� �ε�
    public void LoadCharacterData(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < characterSlots.Count)
        {
            string fileName = repositoryPrefix + slotIndex; // ��: CharacterSlot_0.json
            characterSlots[slotIndex].LoadSlotData(player, fileName);
            Debug.Log($"���� {slotIndex + 1} ������ �ε� �Ϸ�");
        }
        else
        {
            Debug.LogError("�߸��� ���� �ε���");
        }
    }

    // �� ������ ã�� ĳ���͸� ����
    public void SaveCharacterData()
    {
        int availableSlotIndex = GetAvailableSlotIndex(); // ������ ���� ù ��° ������ ã��
        if (availableSlotIndex >= 0)
        {
            string fileName = repositoryPrefix + availableSlotIndex; // ��: CharacterSlot_0.json
            characterSlots[availableSlotIndex].SaveSlotData(player, fileName);
            Debug.Log($"���� {availableSlotIndex + 1} ������ ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("��� ������ �� á���ϴ�.");
        }
    }

    // ������ ���� ù ��° ������ ã�� �޼���
    private int GetAvailableSlotIndex()
    {
        for (int i = 0; i < characterSlots.Count; i++)
        {
            // ���� ���Կ� ĳ���� �����Ͱ� ���ٸ� �� ������ ��ȯ
            if (characterSlots[i].playerData == null)
            {
                return i;
            }
        }
        return -1; // ��� ������ �� ���� -1 ��ȯ
    }
}
