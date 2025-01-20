using MainData;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
{
    [SerializeField] private Image _itemImage; // ���Կ� ǥ�õ� ������ �̹��� �ؽ�Ʈ
    private CraftingData _item;  // ���Կ� �Ҵ�� ������

    private StringBuilder _descText = new();

    // ������ �ʱ�ȭ (�������� ���Կ� ǥ��)
    public void Initialize(CraftingData inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.id);

        _item = inItem;
        _itemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath); // ������ ������ ����
        bool isSprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);

        _itemImage.enabled = true;            // �̹��� ǥ��
        //_name.text = itemData.name;

        gameObject.SetActive(true);          // ���� Ȱ��ȭ
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

        StatText(Stat(itemData.health), itemData.health, "ü��");
        StatText(Stat(itemData.stamina), itemData.stamina, "�ൿ��");
        StatText(Stat(itemData.defense), itemData.defense, "����");
        StatText(Stat(itemData.attack), itemData.attack, "�������ݷ�");
        StatText(Stat(itemData.attackM), itemData.attackM, "�������ݷ�");
        StatText(Stat(itemData.attackMine), itemData.attackMine, "ä�����ݷ�");
        StatText(Stat(itemData.moveSpeed), itemData.moveSpeed, "�̵��ӵ�");
        StatText(Stat(itemData.attackSpeed), itemData.attackSpeed, "���ݼӵ�");

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
