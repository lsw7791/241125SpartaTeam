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

    private void OnEnable()
    {
        Setup(GameManager.Instance.player.inventory);
        _playerName.text = GameManager.Instance.player.nickName;
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
        { // TODO :: �κ��丮�� ���� �״� �ϴ� �������� ���� �ɸ� �� ����
            // UIPool�� ���� �ҷ��;߰���
            // �׷� TMPText ������ �ذ� ���� ������ ����
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slot.AddComponent<Button>();

            _slots.Add(slot);
        }

        itemUseMenu.SetActive(false);
    }

    private void Refresh()
    {
        _hasGold.text = GameManager.Instance.player.stats.Gold.ToString();

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
