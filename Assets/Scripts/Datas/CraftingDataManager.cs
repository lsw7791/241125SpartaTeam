using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataManager : CraftingData
{
    // Ư�� ���� ID�� �ش��ϴ� ũ������ �����͸� ��ȯ�մϴ�.
    public CraftingData GetData(int id)
    {
        if (CraftingDataMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"ID {id}�� �ش��ϴ� ũ������ �����Ͱ� �������� �ʽ��ϴ�.");
        return null;
    }

    // Ư�� Ÿ��(��: ���� ����, Ƽ�� ��)�� ���� �����͸� ��ȯ�մϴ�.
    public List<CraftingData> GetDataByTier(int tier)
    {
        return CraftingDataList.FindAll(data => data.tier == tier);
    }

    // Ư�� ������ ID�� �ʿ��� ��� ������ ��ȯ�մϴ�.
    public Dictionary<int, int> GetRequiredMaterials(int id)
    {
        var data = GetData(id);
        if (data == null)
        {
            return null;
        }

        var materials = new Dictionary<int, int>();

        if (data.resourceMine > 0) materials.Add(1, data.resourceMine);  // �ڿ� 1: ����
        if (data.resourceLadder > 0) materials.Add(2, data.resourceLadder);  // �ڿ� 2: ��ٸ�
        if (data.resourceOther > 0) materials.Add(3, data.resourceOther);  // �ڿ� 3: ��Ÿ
        if (data.resourceJewel > 0) materials.Add(4, data.resourceJewel);  // �ڿ� 4: ����

        return materials;
    }

    // ũ�������� �õ��ϰ� ����� ��ȯ�մϴ�.
    public bool TryCraftItem(int id)
    {
        var requiredMaterials = GetRequiredMaterials(id);

        if (requiredMaterials == null || requiredMaterials.Count == 0)
        {
            Debug.LogWarning($"ID {id}�� �ʿ��� ��� ������ �����ϴ�.");
            return false;
        }

        // �κ��丮���� ��Ḧ Ȯ���մϴ�.
        if (!InventoryManager.Instance.HasRequiredMaterials(requiredMaterials))
        {
            Debug.Log("��ᰡ �����մϴ�!");
            return false;
        }

        // ��� ���� �� ������ ����
        InventoryManager.Instance.ConsumeMaterials(requiredMaterials);
        InventoryManager.Instance.AddItem(id, 1);

        Debug.Log($"������ ID {id} ���� ����!");
        //UIManager.Instance.RefreshUI();  // UI ������Ʈ
        return true;
    }
}
