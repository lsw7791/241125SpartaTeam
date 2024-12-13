using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // ���� UI ���

    public Transform slotContainer; // ���� �θ� ��ü
    public GameObject slotPrefab;   // ���� ������

    public Sprite emptySlotSprite;  // �� ���� �̹���
    [SerializeField] TMP_Text _playerName;
    [SerializeField] TMP_Text _hasGold;

    private void Awake()
    {
        base.Awake();
        _playerName.text = GameManager.Instance.player.NickName;
    }
    //�κ��丮�� ���� �Ӷ����ٰ� �ƴ϶� �ǽð����� ������Ʈ�Ǵ� ����� �����Ǿ����
    private void OnEnable()
    {
        Setup(GameManager.Instance.player.inventory);
    }

    public void Setup(Inventory inventory)
    {
        // ���� �̺�Ʈ ��� ����
        GameManager.Instance.player.inventory.OnInventoryChanged -= Refresh;

        // �� �̺�Ʈ ���
        GameManager.Instance.player.inventory.OnInventoryChanged += Refresh;

        // ���� ���� �� �ʱ�ȭ
        InitializeSlots();
        Refresh(); // �ʱ� UI ����
    }

    private void InitializeSlots()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        for (int i = 0; i < 12; i++) // ���� �� ���� (�ʿ�� ���� ����)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    private void Refresh()
    {
        _hasGold.text = GameManager.Instance.player.stats.Gold.ToString();

        Debug.Log($"Inventory Refresh ȣ�� at {Time.time}");

        var items = GameManager.Instance.player.inventory.GetItems(); // ������ ����Ʈ �޾ƿ���

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
}
