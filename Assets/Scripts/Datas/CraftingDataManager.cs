using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataManager : CraftingData
{
    // Ư�� ������ ID�� �ش��ϴ� ũ������ �����͸� ��ȯ�մϴ�.
    public CraftingData GetData(int id)
    {
        if (CraftingDataMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"ID {id}�� �ش��ϴ� ũ������ �����Ͱ� �������� �ʽ��ϴ�.");
        return null;
    }

    // ��� ũ������ ������ ��ȯ
    public List<CraftingData> GetAllDatas()
    {
        return CraftingDataList;
    }

    // Ư�� Ƽ� �ش��ϴ� �����͸� ��ȯ
    public List<CraftingData> GetDataByTier(int tier)
    {
        return CraftingDataList.FindAll(data => data.tier == tier);
    }

    // Ư�� �����ۿ� �ʿ��� ��� ������ ��ȯ (matter�� count�� �������)
    public Dictionary<int, int> GetRequiredMaterials(int id)
    {
        var data = GetData(id);
        if (data == null) return null;

        var materials = new Dictionary<int, int>();

        // matter�� count ����Ʈ�� ������� ��� ������ �������� �߰�
        List<int> matter = data.matter; // �ڿ� ����
        List<int> count = data.count;   // �ڿ� ����

        for (int i = 0; i < matter.Count; i++)
        {
            int itemId = matter[i];  // �ڿ� ���̵�
            int itemCount = count[i]; // �ش� �ڿ��� �ʿ��� ����

            // ��ᰡ �ִ� ��츸 �߰�
            if (itemCount > 0)
            {
                materials.Add(itemId, itemCount);
            }
        }

        return materials;
    }

    public List<int> GetCraftItemIds(int id)
    {
        return CraftingDataMap[id].matter;
    }

    public List<int> GetCraftCountIds(int id)
    {
        return CraftingDataMap[id].count;
    }
}
