using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // ���� UI ���

    public Transform slotContainer; // ���� �θ� ��ü
    public GameObject slotPrefab;   // ���� ������

    public Sprite emptySlotSprite;  // �� ���� �̹���
    [SerializeField] TMP_Text _playerName;
    [SerializeField] TMP_Text _hasGold;

    public GameObject itemUseMenu;
    
    [Header("Equipment Slots")]
    [SerializeField] private Image _headSlot;
    [SerializeField] private Image _armorSlot;
    [SerializeField] private Image _weaponSlot;
    [SerializeField] private Image _shoesSlot;

    [SerializeField] public Image _shieldSlot;
    [SerializeField] private Image _topSlot;
    [SerializeField] private Image _capeSlot;

    private Dictionary<ItemType, Image> _equipmentSlots;

    protected override void Awake()
    {
        _equipmentSlots = new Dictionary<ItemType, Image>
        {
            { ItemType.Helmet, _headSlot },
            { ItemType.Armor, _armorSlot },
            { ItemType.Weapon, _weaponSlot },
            { ItemType.Bottom, _shoesSlot },
            { ItemType.Shield, _shieldSlot },
            { ItemType.Top, _topSlot },
            { ItemType.Cape, _capeSlot },
        };
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        Setup(GameManager.Instance.Player.inventory);
        _playerName.text = GameManager.Instance.Player.stats.NickName;
        EquipmentRefresh();
    }

    public void Setup(Inventory inventory)
    {
        // ���� �̺�Ʈ ��� ����
        GameManager.Instance.Player.inventory.OnInventoryChanged -= Refresh;

        // �� �̺�Ʈ ���
        GameManager.Instance.Player.inventory.OnInventoryChanged += Refresh;

        // ���� ���� �� �ʱ�ȭ
        InitializeSlots();
        Refresh(); // �ʱ� UI ����
        EquipmentRefresh();
    }

    private void InitializeSlots()
    {
        var items = GameManager.Instance.Player.inventory.GetItems(); // ������ ����Ʈ �޾ƿ���
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        for (int i = 0; i < 12; i++) // ���� �� ���� (�ʿ�� ���� ����)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slot.AddComponent<Button>();

            _slots.Add(slot);
        }

        itemUseMenu.SetActive(false);
    }

    private void Refresh()
    {
        _hasGold.text = GameManager.Instance.Player.stats.Gold.ToString();

        var items = GameManager.Instance.Player.inventory.GetItems(); // ������ ����Ʈ �޾ƿ���

        for (int i = 0; i < _slots.Count; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < items.Count)
            {
                slotComponent.Initialize(items[i]);
            }
            else
            {
                slotComponent.ClearSlot();
            }
        }
    }

    // ���â�� ������ ������ ǥ�� + EquipManager ����
    public void UpdateEquipmentSlot(ItemType inSlot, Sprite itemIcon)
    {
        UpdateSlot(inSlot, itemIcon);
    }

    // ��� ���� �� ���� ���� + EquipManager ����
    public void ClearEquipmentSlot(ItemType inSlot)
    {
        UpdateSlot(inSlot, null);
    }

    private void UpdateSlot(ItemType inSlot, Sprite itemIcon)
    {
        if (_equipmentSlots.TryGetValue(inSlot, out Image outSlotImage))
        {
            outSlotImage.sprite = itemIcon;
            outSlotImage.enabled = itemIcon != null;
        }

        // EquipManager�� �ش� ���� Ŭ���� ��û
        EquipManager.Instance.UpdateEquipPlace(inSlot, itemIcon);
    }

    public void EquipmentUIReset()
    {
        List<ItemType> keys = new List<ItemType>(_equipmentSlots.Keys);

        for (int i = 0; i < _equipmentSlots.Count; i++)
        {
            UpdateSlot(keys[i], null);
        }
    }
    public void EquipmentRefresh()
    {
        EquipmentUIReset();

        var items = GameManager.Instance.Player.inventory.GetItems(); // ������ ����Ʈ �޾ƿ���

        foreach (InventoryItem item in items)
        {
            if (item.IsEquipped)
            {
                var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);
                UpdateSlot(itemData.itemType, UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath));
            }
        }
    }
}
