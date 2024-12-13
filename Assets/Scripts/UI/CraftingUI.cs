using MainData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;

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
    private TMP_Text _productText;

    [SerializeField]
    private Image[] _craftItemImage;
    [SerializeField]
    private TMP_Text[] _craftItemText;

    public void Init(CraftingData inData)
    {
        craftingData = inData;

        Sprite itemSprite = Resources.Load<Sprite>(craftingData.imagePath);
        _productImage.sprite = itemSprite;
        _productText.text = craftingData.name;

        List<int> craftItemList = GameManager.Instance.dataManager.crafting.GetCraftItemIds(craftingData.id);

        for(int i = 0; i < craftItemList.Count; i++)
        {
            _craftItemImage[i].sprite = null;
            _craftItemText[i].TryGetComponent<TMP_Text>(out var outCraftItemText);
            outCraftItemText.text = null;
            _craftItemImage[i].gameObject.SetActive(false);

            if (craftItemList[i] != 0)
            {
                int count = GameManager.Instance.dataManager.crafting.GetCraftCountIds(craftingData.id)[i];
                _craftItemImage[i].gameObject.SetActive(true);
                var itemData = GameManager.Instance.dataManager.GetItemDataById(craftItemList[i]);

                _craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
                outCraftItemText.text = $"{GameManager.Instance.player.inventory.GetItemCount(itemData.id)} / {count}\n{itemData.name}";
            }
        }
    }
}
