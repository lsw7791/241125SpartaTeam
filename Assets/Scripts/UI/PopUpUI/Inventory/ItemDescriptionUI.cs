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
        _CountText.text = itemData.itemType < ItemType.Mine ? $"{inItem.enhenceCount}��" : $"{inItem.Quantity}��";
        _backgroundImage.color = inItem.TierColoer(tierIndex);
        _descriptionText.text = $"{ItemStats(inItem)}";
    }

    private string ItemStats(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        descText.Length = 0;

        descText.Append($"{itemData.desc}\n");

        StatText(Stat(itemData.health), itemData.health, inItem.enhenceCount, "ü��");
        StatText(Stat(itemData.stamina), itemData.stamina, inItem.enhenceCount, "�ൿ��");
        StatText(Stat(itemData.defense), itemData.defense, inItem.enhenceCount, "����");
        StatText(Stat(itemData.attack), itemData.attack, inItem.enhenceCount, "�������ݷ�");
        StatText(Stat(itemData.attackM), itemData.attackM, inItem.enhenceCount, "�������ݷ�");
        StatText(Stat(itemData.attackMine), itemData.attackMine, inItem.enhenceCount, "ä�����ݷ�");
        StatText(Stat(itemData.moveSpeed), itemData.moveSpeed, inItem.enhenceCount, "�̵��ӵ�");
        StatText(Stat(itemData.attackSpeed), itemData.attackSpeed, inItem.enhenceCount, "���ݼӵ�");

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
