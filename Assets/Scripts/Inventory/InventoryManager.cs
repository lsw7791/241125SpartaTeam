using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    
    public bool HasRequiredMaterials(Dictionary<int, int> requiredMaterials)
    {
        // 재료 확인 로직 구현
        return true;
    }

    public void ConsumeMaterials(Dictionary<int, int> requiredMaterials)
    {
        // 재료 차감 로직 구현
    }

    public void AddItem(int id, int quantity)
    {
        // 인벤토리에 아이템 추가 로직 구현
    }
}
