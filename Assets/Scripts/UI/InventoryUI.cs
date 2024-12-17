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

    private void OnEnable()
    {
        Setup(GameManager.Instance.player.inventory);
        _playerName.text = GameManager.Instance.player.nickName;
    }

    public void Setup(Inventory inventory)
    {
        // 기존 이벤트 등록 제거
        GameManager.Instance.player.inventory.OnInventoryChanged -= Refresh;

        // 새 이벤트 등록
        GameManager.Instance.player.inventory.OnInventoryChanged += Refresh;

        // 슬롯 생성 및 초기화
        InitializeSlots();
        Refresh(); // 초기 UI 갱신
    }

    private void InitializeSlots()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        for (int i = 0; i < 12; i++) // 슬롯 수 고정 (필요시 수정 가능)
        { // TODO :: 인벤토리를 껐다 켰다 하는 과정에서 렉이 걸릴 수 있음
            // UIPool을 만들어서 불러와야겠음
            // 그럼 TMPText 문제도 해결 되지 않을까 생각
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slot.AddComponent<Button>();

            _slots.Add(slot);
        }

        itemUseMenu.SetActive(false);
    }

    private void Refresh()
    {
        _hasGold.text = GameManager.Instance.player.stats.Gold.ToString();

        var items = GameManager.Instance.player.inventory.GetItems(); // 아이템 리스트 받아오기

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
