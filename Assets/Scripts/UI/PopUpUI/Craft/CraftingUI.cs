using MainData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftingUI : UIBase
{
    [SerializeField] private CraftingData _craftingData;
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _productText;

    [SerializeField] private Image[] _craftItemImage;
    [SerializeField] private TMP_Text[] _craftItemText;

    [SerializeField] private Button _craftResultButton; // 조합 버튼

    private void Start()
    {
        _craftResultButton.onClick.AddListener(() =>
        {
            // TryCraftItem 메서드를 호출하여 아이템 조합 시도
            bool isSuccess = GameManager.Instance.CraftingManager.TryCraftItem();

            // CraftResultUI의 Sorting Order를 3으로 설정
            UIManager.Instance.ToggleUI<CraftResultUI>();
            UIManager.Instance.ToggleUI<CraftingUI>();
            CraftResultUI craftResultUI = UIManager.Instance.OpenUI<CraftResultUI>();
            craftResultUI.transform.SetAsLastSibling();

            // 결과 표시
            if (isSuccess)
            {
                craftResultUI.ShowSuccess(_craftingData);
            }
            else
            {
                craftResultUI.ShowFailure(_craftingData);
            }
        });
    }

    public void Init(CraftingData inData)
    {
        _craftingData = inData;

        for (int i = 0; i < _craftItemImage.Length; i++)
        {
            _craftItemImage[i].gameObject.SetActive(false);
        }

        Sprite itemSprite = Resources.Load<Sprite>(_craftingData.imagePath);
        _productImage.sprite = itemSprite;
        _productText.text = _craftingData.name;

        List<int> craftItemList = GameManager.Instance.DataManager.Crafting.GetCraftItemIds(_craftingData.id);

        for (int i = 0; i < craftItemList.Count; i++)
        {
            if (craftItemList[i] == 0)
            {
                return;
            }

            _craftItemImage[i].gameObject.SetActive(true);
            var itemData = GameManager.Instance.DataManager.GetItemDataById(craftItemList[i]);

            //_craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
            if(_craftingData.id == 106)
            {
                _craftItemImage[i].sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);

            }
            else
            {
                _craftItemImage[i].sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);

            }
            //_craftItemImage[i].sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
            int craftItemCount = GameManager.Instance.DataManager.Crafting.GetCraftCountIds(_craftingData.id)[i];
            int inventoryItemCount = GameManager.Instance.Player.inventory.GetItemCount(itemData.id);

            _craftItemText[i].text = $"{inventoryItemCount} / {craftItemCount}\n{itemData.name}";
        }
    }
}
