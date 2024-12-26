using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftUI : UIBase
{
    [SerializeField] private Transform _craftingPanel; // 조합창의 부모 패널

    private void Start()
    {
        List<CraftingData> craftingDataList = GameManager.Instance.DataManager.Crafting.GetAllDatas();
        PopulateCraftingUI(craftingDataList); // UI에 아이템을 채우기
    }

    // UI에 아이템 목록을 채우는 메서드
    private void PopulateCraftingUI(List<CraftingData> inDataList)
    {
        // 기존 UI 초기화
        foreach (Transform child in _craftingPanel)
        {
            Destroy(child.gameObject);
        }

        // 데이터 기반으로 UI 생성
        foreach (var data in inDataList)
        {
            GameObject newSlot = SlotObject(data);
            Image itemImage = newSlot.AddComponent<Image>();
            Button slotButton = newSlot.AddComponent<Button>();

            if (slotButton != null)
            {
                itemImage.sprite = Resources.Load<Sprite>(data.imagePath);

                slotButton.onClick.AddListener(() =>
                {
                    // 아이템 선택 시 선택된 아이템 ID를 설정
                    GameManager.Instance.CraftingManager.SelectItem(data.id);

                    // 선택된 아이템을 보여주는 UI를 가져오고, 정렬 순서를 설정
                    UIManager.Instance.ToggleUI<CraftingUI>();
                    CraftingUI craftingUI = UIManager.Instance.OpenUI<CraftingUI>();

                    // 선택된 아이템 데이터를 UI에 초기화
                    craftingUI.Init(data);
                });
            }
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = new GameObject();
        outSlot.transform.parent = _craftingPanel;
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
