using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private int selectedItemId = -1;  // 선택된 아이템의 ID

    // 아이템 선택 시 처리
    public void SelectItem(int itemId)
    {
        if (itemId == -1)
        {
            return;
        }

        selectedItemId = itemId;

        CraftingData selectedData = GameManager.Instance.DataManager.Crafting.GetData(itemId);
    }

    // 선택된 아이템을 조합하는 메서드
    public bool TryCraftItem()
    {
        if (selectedItemId == -1)
        {
            Debug.LogWarning("아이템을 선택해주세요.");
            return false;
        }

        // 선택된 아이템에 필요한 재료를 가져옵니다.
        var requiredMaterials = GameManager.Instance.DataManager.Crafting.GetRequiredMaterials(selectedItemId);

        // 필요한 재료 목록 확인 (0 갯수는 예외 처리)
        List<int> missingMaterials = new List<int>();
        foreach (var material in requiredMaterials)
        {
            if (material.Value > 0) // 재료 수가 0이 아닌 것만 확인
            {
                int playerItemCount = GameManager.Instance.Player.inventory.GetItemCount(material.Key);
                if (playerItemCount < material.Value)
                {
                    missingMaterials.Add(material.Key); // 부족한 재료 추가
                }
            }
        }

        if (missingMaterials.Count > 0)
        {
            Debug.Log("재료가 부족합니다: " + string.Join(", ", missingMaterials));
            return false;
        }

        // 재료 소비 및 아이템 생성
        ConsumeMaterials(requiredMaterials);

        Debug.Log($"아이템 ID {selectedItemId} 제작 성공!");
        return true;
    }

    // 재료 소비
    public void ConsumeMaterials(Dictionary<int, int> requiredMaterials)
    {
        foreach (var material in requiredMaterials)
        {
            if (material.Value > 0)
            {
                GameManager.Instance.Player.inventory.RemoveItem(material.Key, material.Value);
                Debug.Log($"{material.Value}개 {material.Key} 아이템 사용됨.");
            }
        }
    } 

    public void AddToInventory()
    {
        ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(selectedItemId);
        if (itemData != null)
        {
            GameManager.Instance.Player.inventory.AddItem(selectedItemId,1);
            //Debug.Log($"아이템 ID {selectedItemId} 추가됨.");
        }
    }
}
