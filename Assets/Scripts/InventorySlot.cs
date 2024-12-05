using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // ���Կ� ǥ�õ� ������ �̹���

    public void Initialize(ItemInstance item)
    {
        itemImage.sprite = item.icon; // �������� ������ ����
        itemImage.enabled = true;    // �̹��� ǥ��
    }

    public void ClearSlot(Sprite emptySprite)
    {
        itemImage.sprite = emptySprite; // �� ���� �̹��� ����
        itemImage.enabled = true;      // �̹��� ǥ��
    }
}
