using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; // ���Կ� ǥ�õ� ������ �̹���
    [SerializeField] private TMP_Text count;  // ������ ���� �ؽ�Ʈ
    private InventoryItem item;  // ���Կ� �Ҵ�� ������

    // ���� Ŭ�� �� ������ ��� �޴� Ȱ��ȭ
    private void Start()
    {
        Button slotButton = GetComponent<Button>();

        slotButton.onClick.AddListener(() =>
        {
            GameObject itemUseMenu = UIManager.Instance.GetUI<InventoryUI>().itemUseMenu;
            itemUseMenu.SetActive(true);
            itemUseMenu.TryGetComponent<ItemUseUI>(out var outUseUI);
            outUseUI.Initialize(item);  // ������ ��� UI �ʱ�ȭ
        });
    }

    // ������ �ʱ�ȭ (�������� ���Կ� ǥ��)
    public void Initialize(InventoryItem inItem)
    {
        if (inItem == null)
        {
            ClearSlot();
            return;
        }

        item = inItem;
        itemImage.sprite = inItem.ItemIcon;  // ������ ������ ����
        itemImage.enabled = true;            // �̹��� ǥ��
        count.text = inItem.Quantity.ToString();  // ���� ǥ��
        gameObject.SetActive(true);          // ���� Ȱ��ȭ
    }

    // �� ���� ó�� (���� ��Ȱ��ȭ)
    public void ClearSlot()
    {
        itemImage.enabled = false;   // �̹��� �����
        count.text = string.Empty;   // ���� �ؽ�Ʈ ����
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ
    }
}
