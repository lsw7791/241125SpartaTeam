using MainData;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    private int selectedItemId = -1;  // ���õ� �������� ID

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ������ ���� �� ó��
    public void SelectItem(int itemId)
    {
        selectedItemId = itemId;
        Debug.Log($"������ ID {itemId} ���õ�");

        // ���õ� �������� ������ UI�� �ݿ��ϰų� �ٸ� ó���� �� �� �ֽ��ϴ�.
        CraftingData selectedData = DataManager.Instance.crafting.GetData(itemId);
        if (selectedData != null)
        {
            // ���õ� �����ۿ� ���� ���� ǥ�� ���� �߰�
            Debug.Log($"���õ� ������: {selectedData}");
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

        var requiredMaterials = DataManager.Instance.crafting.GetRequiredMaterials(selectedItemId);

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
