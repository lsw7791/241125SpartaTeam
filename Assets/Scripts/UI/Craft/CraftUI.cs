using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // 조합창의 부모 패널
    [SerializeField] private GameObject craftingSlotPrefab; // 조합 슬롯 UI 프리팹

    private void Start()
    {
        // DataManager에서 조합 데이터를 가져와 조합창을 초기화
        List<CraftingData> craftingDataList = GameManager.Instance.dataManager.crafting.GetAllDatas();
        PopulateCraftingUI(craftingDataList); // UI에 아이템을 채우기
    }

    // UI에 아이템 목록을 채우는 메서드
    public void PopulateCraftingUI(List<CraftingData> dataList)
    {
        // 기존 UI 초기화
        foreach (Transform child in craftingPanel)
        {
            Destroy(child.gameObject);
        }

        // 데이터 기반으로 UI 생성
        foreach (var data in dataList)
        {
            GameObject newSlot = SlotObject(data);
            Button slotButton = newSlot.GetComponent<Button>();

            // 슬롯 클릭 시 상세 UI 띄우기
            CraftingSlot craftingSlot = newSlot.GetComponent<CraftingSlot>();
            if (craftingSlot != null)
            {
                craftingSlot.Init(data); // data를 craftingSlot에 전달
            }

            slotButton.onClick.AddListener(() =>
            {
                // 아이템 선택 시 선택된 아이템 ID를 설정
                GameManager.Instance.craftingManager.SelectItem(data.id);

                // 선택된 아이템을 보여주는 UI를 업데이트
                GameManager.Instance.uIManager.OpenUI<CraftingUI>().Init(data);
            });
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = Instantiate(craftingSlotPrefab, craftingPanel);
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
