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
        Debug.Log($"Item set: {itemData.name} - {itemData.desc}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var outPlayer))
        {
            Debug.Log($"Player picked up: {itemData.name}");

            // �������� �κ��丮�� �߰� (��������Ʈ ����)
            Player.Instance.AddItemToInventory(
                itemData.id.ToString(),
                itemData.name,
                1,
                itemData.itemType.ToString(),
                itemSprite
            );

            Destroy(gameObject); // ������ ����
        }
    }
}
