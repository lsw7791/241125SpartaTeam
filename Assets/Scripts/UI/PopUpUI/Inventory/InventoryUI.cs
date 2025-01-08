using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // 슬롯 UI 목록

    public Transform slotContainer; // 슬롯 부모 객체
    public GameObject slotPrefab;   // 슬롯 프리팹

    public Sprite emptySlotSprite;  // 빈 슬롯 이미지
    [SerializeField] TMP_Text _playerName;
    [SerializeField] TMP_Text _hasGold;

    public GameObject itemUseMenu;
    public RectTransform ScrollContent;
    public ScrollRect scrollRect;


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
        scrollRect.onValueChanged.AddListener(LimitScrollPosition);

    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        Setup(GameManager.Instance.Player.inventory);
        _playerName.text = GameManager.Instance.Player.stats.NickName;

        ScrollContent.anchoredPosition = new Vector2(ScrollContent.anchoredPosition.x, 0);
        EquipmentRefresh();
    }

    public void Setup(Inventory inventory)
    {
        // 기존 이벤트 등록 제거
        GameManager.Instance.Player.inventory.OnInventoryChanged -= Refresh;

        // 새 이벤트 등록
        GameManager.Instance.Player.inventory.OnInventoryChanged += Refresh;

        // 슬롯 생성 및 초기화
        InitializeSlots();
        Refresh(); // 초기 UI 갱신
        EquipmentRefresh();
    }

    private void InitializeSlots()
    {
        var item = GameManager.Instance.Player.inventory; // 아이템 리스트 받아오기
        var items = item.GetItems(); // 아이템 리스트 받아오기
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        for (int i = 0; i < item.Items.Count; i++) // 슬롯 수 고정 (필요시 수정 가능)
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

        var items = GameManager.Instance.Player.inventory.GetItems(); // 아이템 리스트 받아오기

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

    // 장비창에 아이템 아이콘 표시 + EquipManager 연동
    public void UpdateEquipmentSlot(ItemType inSlot, Sprite itemIcon)
    {
        UpdateSlot(inSlot, itemIcon);
    }

    // 장비 해제 시 슬롯 비우기 + EquipManager 연동
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

        // EquipManager에 해당 슬롯 클리어 요청
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
    private void LimitScrollPosition(Vector2 normalizedPosition)
    {
        // 현재 ScrollContent의 위치 가져오기
        Vector2 currentPos = ScrollContent.anchoredPosition;

        // y값이 0 이하로 내려가지 않도록 제한
        if (currentPos.y < 0)
        {
            currentPos.y = 0;
            ScrollContent.anchoredPosition = currentPos;
        }
    }


    public void EquipmentRefresh()
    {
        EquipmentUIReset();

        var items = GameManager.Instance.Player.inventory.GetItems(); // 아이템 리스트 받아오기

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
