using MainData;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private int selectedItemId = -1;  // 선택된 아이템의 ID

    // 아이템 선택 시 처리
    public void SelectItem(int itemId)
    {
        selectedItemId = itemId;
        Debug.Log($"아이템 ID {itemId} 선택됨");

        // 크래프팅 데이터를 가져옴
        CraftingData selectedData = GameManager.Instance.dataManager.crafting.GetData(itemId);
        if (selectedData != null)
        {
            // 선택된 아이템에 대한 정보 표시

            // 조합을 시도해봄
            if (TryCraftItem())
            {
                Debug.Log($"아이템 ID {itemId} 제작 성공!");
            }
            else
            {
                Debug.Log("아이템 제작 실패 또는 재료 부족.");
            }
        }
        else
        {
            Debug.LogWarning("선택된 아이템이 유효하지 않습니다.");
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

        // 필요한 재료 정보를 가져옴
        var requiredMaterials = GameManager.Instance.dataManager.crafting.GetRequiredMaterials(selectedItemId);

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

        // 재료 소비
        InventoryManager.Instance.ConsumeMaterials(requiredMaterials);

        // 아이템 데이터 가져오기
        ItemData selectedItem = GameManager.Instance.dataManager.item.GetItemDataById(selectedItemId);

        if (selectedItem != null)
        {
            // 아이템 추가 (이미지 포함)
            GameManager.Instance.player.inventory.AddItem(
                selectedItem.id.ToString(),  // 아이템 ID
                selectedItem.name,       // 아이템 이름
                1,                           // 아이템 수량
                selectedItem.itemType.ToString(), // 아이템 타입
                selectedItem.itemSprite      // 아이템 이미지
            );

            Debug.Log($"아이템 ID {selectedItemId} 제작 성공!");
            return true;
        }
        else
        {
            Debug.LogWarning($"아이템 ID {selectedItemId}에 해당하는 아이템 데이터가 없습니다.");
            return false;
        }
    }
}
