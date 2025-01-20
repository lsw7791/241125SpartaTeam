using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CraftUI : UIBase
{
    [SerializeField] private Transform _craftingPanel; // 조합창의 부모 패널
    public ScrollRect scrollRect;

    [Header("DescriptionUI")]
    public GameObject itemObject;
    public Image itemImage; // 슬롯에 표시될 아이템 이미지
    public TMP_Text itemName;  // 아이템 이름 텍스트
    public TMP_Text itemDescription;

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
            //CraftSlot craftSlot = newSlot.AddComponent<CraftSlot>();
            //Image itemImage = newSlot.AddComponent<Image>();
            Button slotButton = newSlot.AddComponent<Button>();

            if (slotButton != null)
            {
                newSlot.TryGetComponent<CraftSlot>(out var outCraftSlot);

                outCraftSlot.Initialize(data);
                //itemImage.sprite = Resources.Load<Sprite>(data.imagePath);
                //itemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(data.atlasPath);
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
        //GameObject outSlot = new GameObject();
        var slotPrefab = Resources.Load<GameObject>("Prefabs/Items/CraftSlot");

        GameObject outSlot = Instantiate(slotPrefab, _craftingPanel);
        //outSlot.transform.parent = _craftingPanel;
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
