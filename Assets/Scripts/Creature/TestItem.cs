using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����
    public Sprite itemSprite; // �������� ��������Ʈ (�̹���)

    // ������ �����͸� �����ϴ� �޼���
    public void SetData(ItemData data, Sprite sprite)
    {
        itemData = data;
        itemSprite = sprite; // ��������Ʈ ������ ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾���� �浹���� Ȯ��
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Player.AddItemToInventory(
                itemData.id,
                itemData.name,
                1,
                itemData.itemType,
                itemSprite
            );

            Destroy(gameObject); // ������ ����
        }
    }

}
