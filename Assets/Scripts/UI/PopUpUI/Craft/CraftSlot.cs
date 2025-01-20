using MainData;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
{
    [SerializeField] private Image _itemImage; // 슬롯에 표시될 아이템 이미지 텍스트
    private CraftingData _item;  // 슬롯에 할당된 아이템

    private StringBuilder _descText = new();

    // 아이템 초기화 (아이템을 슬롯에 표시)
    public void Initialize(CraftingData inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.id);

        _item = inItem;
        _itemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath); // 아이템 아이콘 설정
        bool isSprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);

        _itemImage.enabled = true;            // 이미지 표시
        //_name.text = itemData.name;

        gameObject.SetActive(true);          // 슬롯 활성화
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(_item.id);

        CraftUI craftUI = UIManager.Instance.GetUI<CraftUI>();

        craftUI.itemObject.SetActive(true);
        craftUI.itemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        craftUI.itemName.text = itemData.name;

        craftUI.itemDescription.text = $"{ItemStats(_item)}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CraftUI craftUI = UIManager.Instance.GetUI<CraftUI>();

        craftUI.itemObject.SetActive(false);
    }

    public void OnScroll(PointerEventData eventData)
    {
        CraftUI craftUI = UIManager.Instance.GetUI<CraftUI>();

        if (craftUI.scrollRect != null)
        {
            craftUI.scrollRect.OnScroll(eventData);
        }
    }

    private string ItemStats(CraftingData inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.id);
        _descText.Length = 0;

        StatText(Stat(itemData.health), itemData.health, "체력");
        StatText(Stat(itemData.stamina), itemData.stamina, "행동력");
        StatText(Stat(itemData.defense), itemData.defense, "방어력");
        StatText(Stat(itemData.attack), itemData.attack, "물리공격력");
        StatText(Stat(itemData.attackM), itemData.attackM, "마법공격력");
        StatText(Stat(itemData.attackMine), itemData.attackMine, "채광공격력");
        StatText(Stat(itemData.moveSpeed), itemData.moveSpeed, "이동속도");
        StatText(Stat(itemData.attackSpeed), itemData.attackSpeed, "공격속도");

        return _descText.ToString();
    }

    private bool Stat(float inItemData)
    {
        return inItemData != 0;
    }

    private void StatText(bool inIsStat, float inItemData, string inStatName)
    {
        if (inIsStat)
        {
            _descText.Append($"{inStatName} : {inItemData * 1}\n");
        }
    }
}
