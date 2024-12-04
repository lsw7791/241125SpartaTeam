using MainData;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    private int selectedItemId = -1;  // 선택된 아이템의 ID

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 아이템 선택 시 처리
    public void SelectItem(int itemId)
    {
        selectedItemId = itemId;
        Debug.Log($"아이템 ID {itemId} 선택됨");

        // 선택된 아이템의 정보를 UI에 반영하거나 다른 처리를 할 수 있습니다.
        CraftingData selectedData = DataManager.Instance.crafting.GetData(itemId);
        if (selectedData != null)
        {
            // 선택된 아이템에 대한 정보 표시 등을 추가
            Debug.Log($"선택된 아이템: {selectedData}");
        }
    }

    // 선택된 아이템을 조합하는 메서드
    public bool TryCraftItem()
    {
        if (selectedItemId == -1)
        {
            Debug.LogWarning("아이템을 선택해주세요.");
            return false;
        }

        var requiredMaterials = DataManager.Instance.crafting.GetRequiredMaterials(selectedItemId);

        if (requiredMaterials == null || requiredMaterials.Count == 0)
        {
            Debug.LogWarning($"ID {selectedItemId}에 필요한 재료 정보가 없습니다.");
            return false;
        }

        // 재료 확인
        if (!InventoryManager.Instance.HasRequiredMaterials(requiredMaterials))
        {
            Debug.Log("재료가 부족합니다!");
            return false;
        }

        // 재료 소비 및 아이템 생성
        InventoryManager.Instance.ConsumeMaterials(requiredMaterials);
        InventoryManager.Instance.AddItem(selectedItemId, 1);

        Debug.Log($"아이템 ID {selectedItemId} 제작 성공!");
        return true;
    }
}
