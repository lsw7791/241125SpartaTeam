using MainData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftingUI : UIBase
{
    [SerializeField] private CraftingData craftingData;
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _productText;

    [SerializeField] private Image[] _craftItemImage;
    [SerializeField] private TMP_Text[] _craftItemText;

    [SerializeField] private Button craftResultButton; // 조합 버튼

    private void Start()
    {
        craftResultButton.onClick.AddListener(() =>
        {
            Debug.Log("조합 버튼 클릭됨.");

            // TryCraftItem 메서드를 호출하여 아이템 조합 시도
            if (GameManager.Instance.craftingManager.TryCraftItem())
            {
                // 조합 성공
                GameManager.Instance.uIManager.CloseUI<CraftingUI>();
                CraftResultUI craftResultUI = GameManager.Instance.uIManager.OpenUI<CraftResultUI>();
                craftResultUI.ShowSuccess(craftingData);
            }
            else
            {
                // 조합 실패
                GameManager.Instance.uIManager.CloseUI<CraftingUI>();
                CraftResultUI craftResultUI = GameManager.Instance.uIManager.OpenUI<CraftResultUI>();
                craftResultUI.ShowFailure(craftingData);
            }
        });
    }

    public void Init(CraftingData inData)
    {
        craftingData = inData;

        for (int i = 0; i < _craftItemImage.Length; i++)
        {
            _craftItemImage[i].gameObject.SetActive(false);
        }

        Sprite itemSprite = Resources.Load<Sprite>(craftingData.imagePath);
        _productImage.sprite = itemSprite;
        _productText.text = craftingData.name;

        List<int> craftItemList = GameManager.Instance.dataManager.crafting.GetCraftItemIds(craftingData.id);

        for (int i = 0; i < craftItemList.Count; i++)
        {
            _craftItemImage[i].sprite = null;
            _craftItemText[i].text = null;

            if (craftItemList[i] != 0)
            {
                int count = GameManager.Instance.dataManager.crafting.GetCraftCountIds(craftingData.id)[i];
                _craftItemImage[i].gameObject.SetActive(true);
                var itemData = GameManager.Instance.dataManager.GetItemDataById(craftItemList[i]);

                _craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
                _craftItemText[i].text = $"{GameManager.Instance.player.inventory.GetItemCount(itemData.id)} / {count}\n{itemData.name}";
            }
        }
    }

    private int Craft(int inCraftResultCount, int inCraftItemCount, int inInventoryItemCount)
    {
        if (inCraftItemCount == inInventoryItemCount)
        {
            inCraftResultCount++;
        }
        return inCraftResultCount;
    }
}
