using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����
    //public Sprite itemSprite; // �������� ��������Ʈ (�̹���)
    private int minGold; // �ּ� ���
    private int maxGold; // �ִ� ���
    public bool isPlayerDrop = false;

    private void Start()
    {
        if(isPlayerDrop)
        {
            Invoke("PlayerDropItem", 5);
        }
    }

    private void PlayerDropItem()
    {
        isPlayerDrop = false;
    }

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
        if (collision.CompareTag("Player") && !isPlayerDrop)
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
                    itemData.atlasPath
                );
                if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(7) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[7])
                {
                    // QuestCompletionStatus[1]�� false�� ���� CompleteQuest(1) ȣ��
                    GameManager.Instance.DataManager.MainQuest.CompleteQuest(7);
                }
            }

            Destroy(gameObject); // ������ ����
        }
    }
}
