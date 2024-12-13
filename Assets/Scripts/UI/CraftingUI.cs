using MainData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : UIBase
{
    //[SerializeField] private Transform craftingPanel; // 조합창의 부모 패널
    //[SerializeField] private GameObject craftingSlotPrefab; // 조합 슬롯 UI 프리팹
    //[SerializeField] private List<CraftingData> craftingDataList; // 아이템 데이터 목록

    //private void Start()
    //{
    //    // 아이템 데이터가 없으면 기본 데이터 할당
    //    if (craftingDataList == null || craftingDataList.Count == 0)
    //    {
    //        craftingDataList = new List<CraftingData>
    //        {
    //            new CraftingData { id = 1, tier = 1, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_1" },
    //            new CraftingData { id = 2, tier = 2, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_2" },
    //            new CraftingData { id = 3, tier = 3, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_3" },
    //            new CraftingData { id = 4, tier = 4, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_4" },
    //            new CraftingData { id = 5, tier = 5, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_5" },
    //            new CraftingData { id = 6, tier = 6, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_6" }
    //        };
    //    }

    //    //PopulateCraftingUI(craftingDataList);  // UI에 아이템을 채우기
    //}

    /*
     * 조합 완성템 데이터
     * 조합 확률
     * 재료 아이템 (인벤토리 데이터)
     * 완성 아이템의 티어 별로 재료 아이템 티어 받아오기
     * 조합 버튼
     * 되돌아가기 버튼
     */
    [SerializeField]
    private CraftingData craftingData;
    [SerializeField]
    private Image _productImage;


    [SerializeField]
    private Image[] _craftItemImage;

    public void Init(CraftingData inData)
    {
        craftingData = inData;

        Debug.Log(craftingData.name);
        Sprite itemSprite = Resources.Load<Sprite>(craftingData.imagePath);
        _productImage.sprite = itemSprite;

        for (int i = 0; i < _craftItemImage.Length; i++)
        {

            foreach (int itemId in GameManager.Instance.dataManager.crafting.GetCraftItemIds(craftingData.id))
            {
                if (itemId != 0)
                {
                    _craftItemImage[i].gameObject.SetActive(true);
                    _craftItemImage[i].sprite = null;
                    var itemData = GameManager.Instance.dataManager.GetItemDataById(itemId);

                    _craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
                }
                else
                {
                    _craftItemImage[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
