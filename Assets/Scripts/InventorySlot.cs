using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; // ���Կ� ǥ�õ� ������ �̹���
    [SerializeField] private TMP_Text count;
    private InventoryItem item;

    private void Start()
    {
        Button slotButton = GetComponent<Button>();

        slotButton.onClick.AddListener(() =>
        {
            GameObject itemUseMenu = UIManager.Instance.GetUI<InventoryUI>().itemUseMenu;
            itemUseMenu.SetActive(true);
            itemUseMenu.TryGetComponent<ItemUseUI>(out var outUseUI);
            outUseUI.Initialize(item);
        });
    }

    private void Update()
    {
        if (item != null)
        {
            UpdateSlot();
        }
    }

    // ������ �ʱ�ȭ
    public void Initialize(InventoryItem inItem)
    {
        itemImage.sprite = inItem.ItemIcon;  // �������� ������ ����
        itemImage.enabled = true;            // �̹��� ǥ��
        int quantity = inItem.Quantity;
        count.text = quantity.ToString();
        item = inItem;
    }

    // �� ���� ó��
    public void ClearSlot()
    {
        gameObject.SetActive(false);  // �� ���� �̹��� ����
    }

    private void UpdateSlot()
    {
        Initialize(item);
        // ������ ���� �ʱ�ȭ

        if (item.IsEquipped)
        { // �������� ���� ���� ��
            count.text = "�����";
        }
        else
        {
            int quantity = item.Quantity;
            count.text = quantity.ToString();
        }
    }
}
