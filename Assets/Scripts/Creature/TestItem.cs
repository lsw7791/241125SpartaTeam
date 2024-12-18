using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����
    public Sprite itemSprite; // �������� ��������Ʈ (�̹���)
    private int minGold; // �ּ� ���
    private int maxGold; // �ִ� ���

    // ������ �����͸� �����ϴ� �޼��� (����)
    public void SetData(ItemData data, int minGold, int maxGold)
    {
        itemData = data;
        this.minGold = minGold;
        this.maxGold = maxGold;
    }

    // ������ �����͸� �����ϴ� �޼��� (�Ϲ� �����ۿ�)
    public void SetData(ItemData data)
    {
        itemData = data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (itemData.itemType == ItemType.Gold) // ��� ���������� Ȯ��
            {
                int randomGold = Random.Range(minGold, maxGold + 1); // ��� �������� ���� ��
                GameManager.Instance.Player.Stats.Gold += randomGold; // �÷��̾� ��� �߰�
                Debug.Log($"{randomGold} ��带 ȹ���߽��ϴ�! ���� ���: {GameManager.Instance.Player.Stats.Gold}");
            }
            else
            {
                // �Ϲ� �������� �κ��丮�� �߰�
                GameManager.Instance.Player.AddItemToInventory(
                    itemData.id,
                    1,
                    itemData.spritePath
                );
            }

            Destroy(gameObject); // ������ ����
        }
    }
}
