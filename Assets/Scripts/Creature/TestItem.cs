using UnityEngine;
using MainData;
using Unity.VisualScripting;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터 저장

    // 아이템 데이터를 설정하는 메서드
    public void SetData(ItemData data)
    {
        itemData = data;
        Debug.Log($"Item set: {itemData.name} - {itemData.desc}");
    }

    // 아이템이 드랍될 위치 설정
    public void DropPosition(Vector2 inDropPosition)
    {
        float randomXPosition = Random.Range(-1f, 1f); // -1 ~ 1 사이의 랜덤 위치
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
