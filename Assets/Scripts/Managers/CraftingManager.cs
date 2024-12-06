using MainData;
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
        if (!InventoryManager.Instance.HasRequiredMaterials(requiredMaterials))
        {
            Debug.Log("��ᰡ �����մϴ�!");
            return false;
        }

        // ��� �Һ� �� ������ ����
        InventoryManager.Instance.ConsumeMaterials(requiredMaterials);
        InventoryManager.Instance.AddItem(selectedItemId, 1);

        Debug.Log($"������ ID {selectedItemId} ���� ����!");
        return true;
    }
}
