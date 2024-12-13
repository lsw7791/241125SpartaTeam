using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Tripolygon.UModeler.UI.ViewModels;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // 조합창의 부모 패널
    //[SerializeField] private GameObject craftingSlotPrefab; // 조합 슬롯 UI 프리팹

    private void Start()
    { // DataManager에서 조합 데이터를 가져와 조합창을 초기화
        List<CraftingData> craftingDataList = GameManager.Instance.dataManager.crafting.GetAllDatas();

        if (craftingDataList == null || craftingDataList.Count == 0)
        {
            Debug.LogError("조합 데이터가 없습니다.");
            return;
        }

        PopulateCraftingUI(craftingDataList); // UI에 아이템을 채우기
    }

    // UI에 아이템 목록을 채우는 메서드
    public void PopulateCraftingUI(List<CraftingData> dataList)
    { // 기존 UI를 초기화한 뒤, 조합 데이터를 기반으로 조합 슬롯 UI를 생성
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

            // 슬롯에 아이템 데이터 적용
            CraftingSlot slotScript = newSlot.AddComponent<CraftingSlot>();
            slotScript.Init(data);

            if (slotScript != null)
            {
                // 데이터에서 경로를 기반으로 스프라이트 로드
                Sprite itemSprite = Resources.Load<Sprite>(data.imagePath);

                if (itemSprite != null)
                {
                    if (itemImage != null && itemSprite != null)
                    {
                        itemImage.sprite = itemSprite;
                    }
                }
                else
                {
                    Debug.LogWarning($"이미지를 경로 '{data.imagePath}'에서 로드할 수 없습니다.");
                }
            }
            else
            {
                Debug.LogError("CraftingSlot 스크립트가 프리팹에 없습니다.");
            }
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = new GameObject();
        outSlot.name = $"{inData.name} Slot";

        outSlot.transform.parent = craftingPanel;
        outSlot.AddComponent<Button>();

        return outSlot;
    }
}
