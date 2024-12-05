using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // ���Կ� ǥ�õ� ������ �̹���

    // ������ �ʱ�ȭ
    public void Initialize(InventoryItem item)
    {
        itemImage.sprite = item.ItemIcon;  // �������� ������ ����
        itemImage.enabled = true;            // �̹��� ǥ��

    }

    // �� ���� ó��
    public void ClearSlot(Sprite emptySprite)
    {
        itemImage.sprite = emptySprite;   // �� ���� �̹��� ����
        itemImage.enabled = true;         // �̹��� ǥ��
    }
}
