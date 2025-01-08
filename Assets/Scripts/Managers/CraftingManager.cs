using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private int selectedItemId = -1;  // ���õ� �������� ID

    // ������ ���� �� ó��
    public void SelectItem(int itemId)
    {
        if (itemId == -1)
        {
            return;
        }

        selectedItemId = itemId;

        CraftingData selectedData = GameManager.Instance.DataManager.Crafting.GetData(itemId);
    }

    // ���õ� �������� �����ϴ� �޼���
    public bool TryCraftItem()
    {
        if (selectedItemId == -1)
        {
            Debug.LogWarning("�������� �������ּ���.");
            return false;
        }

        // ���õ� �����ۿ� �ʿ��� ��Ḧ �����ɴϴ�.
        var requiredMaterials = GameManager.Instance.DataManager.Crafting.GetRequiredMaterials(selectedItemId);

        // �ʿ��� ��� ��� Ȯ�� (0 ������ ���� ó��)
        List<int> missingMaterials = new List<int>();
        foreach (var material in requiredMaterials)
        {
            if (material.Value > 0) // ��� ���� 0�� �ƴ� �͸� Ȯ��
            {
                int playerItemCount = GameManager.Instance.Player.inventory.GetItemCount(material.Key);
                if (playerItemCount < material.Value)
                {
                    missingMaterials.Add(material.Key); // ������ ��� �߰�
                }
            }
        }

        if (missingMaterials.Count > 0)
        {
            Debug.Log("��ᰡ �����մϴ�: " + string.Join(", ", missingMaterials));
            return false;
        }

        // ��� �Һ� �� ������ ����
        ConsumeMaterials(requiredMaterials);

        Debug.Log($"������ ID {selectedItemId} ���� ����!");
        return true;
    }

    // ��� �Һ�
    public void ConsumeMaterials(Dictionary<int, int> requiredMaterials)
    {
        foreach (var material in requiredMaterials)
        {
            if (material.Value > 0)
            {
                GameManager.Instance.Player.inventory.RemoveItem(material.Key, material.Value);
                Debug.Log($"{material.Value}�� {material.Key} ������ ����.");
            }
        }
    } 

    public void AddToInventory()
    {
        ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(selectedItemId);
        if (itemData != null)
        {
            GameManager.Instance.Player.inventory.AddItem(selectedItemId,1);
            //Debug.Log($"������ ID {selectedItemId} �߰���.");
        }
    }
}
