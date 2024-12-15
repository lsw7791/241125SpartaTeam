using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // 조합창의 부모 패널

    private void Start()
    {
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
            Image itemImage = newSlot.AddComponent<Image>();
            Button slotButton = newSlot.AddComponent<Button>();

            if (slotButton != null)
            {
                itemImage.sprite = Resources.Load<Sprite>(data.imagePath);
            }

            slotButton.onClick.AddListener(() =>
            {
                // 아이템 선택 시 선택된 아이템 ID를 설정
                GameManager.Instance.craftingManager.SelectItem(data.id);

                // 선택된 아이템을 보여주는 UI를 업데이트
                CraftingUI craftingUI = GameManager.Instance.uIManager.OpenUI<CraftingUI>();
                craftingUI.Init(data);
            });
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = new GameObject();
        outSlot.transform.parent = craftingPanel;
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
