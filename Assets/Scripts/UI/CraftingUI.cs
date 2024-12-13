using MainData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private Transform craftingPanel; // 조합창의 부모 패널
    [SerializeField] private GameObject craftingSlotPrefab; // 조합 슬롯 UI 프리팹
    [SerializeField] private List<CraftingData> craftingDataList; // 아이템 데이터 목록

    private void Start()
    {
        // 아이템 데이터가 없으면 기본 데이터 할당
        if (craftingDataList == null || craftingDataList.Count == 0)
        {
            craftingDataList = new List<CraftingData>
            {
                new CraftingData { id = 1, tier = 1, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_1" },
                new CraftingData { id = 2, tier = 2, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_2" },
                new CraftingData { id = 3, tier = 3, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_3" },
                new CraftingData { id = 4, tier = 4, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_4" },
                new CraftingData { id = 5, tier = 5, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_5" },
                new CraftingData { id = 6, tier = 6, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_6" }
            };
        }

        //PopulateCraftingUI(craftingDataList);  // UI에 아이템을 채우기
    }

    // UI에 아이템 목록을 채우는 메소드
    //public void PopulateCraftingUI(List<CraftingData> dataList)
    //{
    //    // 기존 UI 초기화
    //    foreach (Transform child in craftingPanel)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // 데이터 기반으로 UI 생성
    //    foreach (var data in dataList)
    //    {
    //        // 슬롯 생성
    //        GameObject slot = Instantiate(craftingSlotPrefab, craftingPanel);

    //        // 슬롯에 아이템 데이터 적용
    //        CraftingSlot slotScript = slot.GetComponent<CraftingSlot>();
    //        if (slotScript != null)
    //        {
    //            slotScript.Setup(data); // 슬롯에 아이템 설정
    //        }
    //        else
    //        {
    //            Debug.LogError("CraftingSlot 스크립트가 프리팹에 없습니다.");
    //        }
    //    }
    //}
}
