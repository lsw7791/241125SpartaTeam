using UnityEngine;
using MainData;
using Unity.VisualScripting;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����

    // ������ �����͸� �����ϴ� �޼���
    public void SetData(ItemData data)
    {
        itemData = data;
        Debug.Log($"Item set: {itemData.name} - {itemData.desc}");
    }

    // �������� ����� ��ġ ����
    public void DropPosition(Vector2 inDropPosition)
    {
        float randomXPosition = Random.Range(-1f, 1f); // -1 ~ 1 ������ ���� ��ġ
        float randomYPosition = Random.Range(-1f, 1f);

        transform.position = new Vector2(inDropPosition.x + randomXPosition, inDropPosition.y + randomYPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var outPlayer))
        {
            Debug.Log($"Player picked up: {itemData.name}");
            Destroy(gameObject);
        }
    }
}
