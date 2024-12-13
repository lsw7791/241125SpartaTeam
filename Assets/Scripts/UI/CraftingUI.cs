using MainData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : UIBase
{
    //[SerializeField] private Transform craftingPanel; // ����â�� �θ� �г�
    //[SerializeField] private GameObject craftingSlotPrefab; // ���� ���� UI ������
    //[SerializeField] private List<CraftingData> craftingDataList; // ������ ������ ���

    //private void Start()
    //{
    //    // ������ �����Ͱ� ������ �⺻ ������ �Ҵ�
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

    //    //PopulateCraftingUI(craftingDataList);  // UI�� �������� ä���
    //}

    /*
     * ���� �ϼ��� ������
     * ���� Ȯ��
     * ��� ������ (�κ��丮 ������)
     * �ϼ� �������� Ƽ�� ���� ��� ������ Ƽ�� �޾ƿ���
     * ���� ��ư
     * �ǵ��ư��� ��ư
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
