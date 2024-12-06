using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // ���Կ� ǥ�õ� ������ �̹���
    public TextMeshProUGUI count;
    

    // ������ �ʱ�ȭ
    public void Initialize(InventoryItem item)
    {
        itemImage.sprite = item.ItemIcon;  // �������� ������ ����
        itemImage.enabled = true;            // �̹��� ǥ��
        int quantity = item.Quantity;
        count.text = quantity.ToString();
    }

    // �� ���� ó��
    public void ClearSlot()
    {
        gameObject.SetActive(false);  // �� ���� �̹��� ����
    }
}
