using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    protected override void Awake()
    {
        base.Awake();
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
