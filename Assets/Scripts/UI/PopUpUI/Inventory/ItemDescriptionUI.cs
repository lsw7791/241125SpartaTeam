using MainData;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : UIBase
{
    [SerializeField] private Image _useItemImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _CountText;
    [SerializeField] private TMP_Text _productText;

    [SerializeField] private TMP_Text _descriptionText;
    StringBuilder descText = new();

    public void Initialize(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        _useItemImage.sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
        int tierIndex = itemData.tier - 1;
        _CountText.text = itemData.itemType < ItemType.Mine ? $"{inItem.enhenceCount}°­" : $"{inItem.Quantity}°³";
        _backgroundImage.color = inItem.TierColoer(tierIndex);
        _descriptionText.text = $"{descText.ToString()}";
    }

    private string ItemStats(ItemData inItemData)
    {//itemData.desc}\n{ItemStats(inItem)}\n{itemData.addDesc
        descText.Append($"{inItemData.desc}");
        return "";
    }
}
