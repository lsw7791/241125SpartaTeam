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
    [SerializeField] private TMP_Text _productNameText;

    [SerializeField] private TMP_Text _descriptionText;
    StringBuilder descText = new();

    public void Initialize(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        _useItemImage.sprite = itemData.itemType < ItemType.Mine ? UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath)
            : UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        int tierIndex = itemData.tier - 1;
        _productNameText.text = $"{itemData.name}";
        _CountText.text = itemData.itemType < ItemType.Mine ? $"{inItem.enhenceCount}강" : $"{inItem.Quantity}개";
        _backgroundImage.color = inItem.TierColoer(tierIndex);
        _descriptionText.text = $"{ItemStats(inItem)}";
    }

    private string ItemStats(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        descText.Length = 0;

        descText.Append($"{itemData.desc}\n");

        StatText(Stat(itemData.health), itemData.health, inItem.enhenceCount, "체력");
        StatText(Stat(itemData.stamina), itemData.stamina, inItem.enhenceCount, "행동력");
        StatText(Stat(itemData.defense), itemData.defense, inItem.enhenceCount, "방어력");
        StatText(Stat(itemData.attack), itemData.attack, inItem.enhenceCount, "물리공격력");
        StatText(Stat(itemData.attackM), itemData.attackM, inItem.enhenceCount, "마법공격력");
        StatText(Stat(itemData.attackMine), itemData.attackMine, inItem.enhenceCount, "채광공격력");
        StatText(Stat(itemData.moveSpeed), itemData.moveSpeed, inItem.enhenceCount, "이동속도");
        StatText(Stat(itemData.attackSpeed), itemData.attackSpeed, inItem.enhenceCount, "공격속도");

        descText.Append($"{itemData.addDesc}");

        return descText.ToString();
    }

    private bool Stat(float inItemData)
    {
        return inItemData != 0;
    }

    private void StatText(bool inIsStat, float inItemData, int inEnhenceCount , string inStatName)
    {
        if (inIsStat)
        {
            descText.Append($"{inStatName} : {inItemData * (inEnhenceCount + 1)}\n");
        }
    }
}
