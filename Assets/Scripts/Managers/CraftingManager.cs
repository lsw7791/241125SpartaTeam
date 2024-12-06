using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private int selectedItemId = -1;  // ���õ� �������� ID

    // ������ ���� �� ó��
    public void SelectItem(int itemId)
    {
        selectedItemId = itemId;
        Debug.Log($"������ ID {itemId} ���õ�");

        // ���õ� �������� ������ UI�� �ݿ��ϰų� �ٸ� ó���� �� �� �ֽ��ϴ�.
        CraftingData selectedData = GameManager.Instance.dataManager.crafting.GetData(itemId);
        if (selectedData != null)
        {
            // ���õ� �����ۿ� ���� ���� ǥ��
            Debug.Log($"���õ� ������: {selectedData}");

            // ������ �õ��غ�
            if (TryCraftItem())
            {
                Debug.Log($"������ ID {itemId} ���� ����!");
            }
            else
            {
                Debug.Log("������ ���� ���� �Ǵ� ��� ����.");
            }
        }
        else
        {
            Debug.LogWarning("���õ� �������� ��ȿ���� �ʽ��ϴ�.");
        }
    }

    // ���õ� �������� �����ϴ� �޼���
    public bool TryCraftItem()
    {
        if (selectedItemId == -1)
        {
            Debug.LogWarning("�������� �������ּ���.");
            return false;
        }

        // �ʿ��� ��� ������ ������
        var requiredMaterials = GameManager.Instance.dataManager.crafting.GetRequiredMaterials(selectedItemId);

        if (requiredMaterials == null || requiredMaterials.Count == 0)
        {
            Debug.LogWarning($"ID {selectedItemId}�� �ʿ��� ��� ������ �����ϴ�.");
            return false;
        }

        // ��� Ȯ��
        if (!HasRequiredMaterials(requiredMaterials))
        {
            Debug.Log("��ᰡ �����մϴ�!");
            return false;
        }

        // ��� �Һ� �� ������ ����
        ConsumeMaterials(requiredMaterials);
        AddItem(selectedItemId, 1);

        Debug.Log($"������ ID {selectedItemId} ���� ����!");
        return true;
    }
    public bool HasRequiredMaterials(Dictionary<int, int> requiredMaterials)
    {
        // ��� Ȯ�� ���� ����
        return true;
    }

    public void ConsumeMaterials(Dictionary<int, int> requiredMaterials)
    {
        // ��� ���� ���� ����
    }

    public void AddItem(int id, int quantity)
    {
        // �κ��丮�� ������ �߰� ���� ����
    }
}
