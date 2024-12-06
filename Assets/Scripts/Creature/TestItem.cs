using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����
    public string imagePath; // �������� ������ ��� (�̹��� ���)

    // ������ �����͸� �����ϴ� �޼���
    public void SetData(ItemData data, string iconPath)
    {
        itemData = data;
        imagePath = iconPath; // �̹��� ��� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾���� �浹���� Ȯ��
        if (collision.CompareTag("Player"))
        {
            // �������� �̹����� ��θ� ���� �ε�
            Sprite itemSprite = LoadItemIcon(imagePath);

            // �������� �κ��丮�� �߰�
            GameManager.Instance.player.AddItemToInventory(
                itemData.id.ToString(),
                itemData.name,
                1,
                itemData.itemType.ToString(),
                itemSprite
            );

            Destroy(gameObject); // ������ ����
        }
    }

    // �������� ��η� �ε��ϴ� �Լ�
    private Sprite LoadItemIcon(string imagePath)
    {
        // ��ΰ� ��������� null ��ȯ
        if (string.IsNullOrEmpty(imagePath))
        {
            return null;
        }

        // Resources ���� ������ ������ �ε�
        Sprite icon = Resources.Load<Sprite>(imagePath);

        if (icon == null)
        {
            Debug.LogWarning($"�������� �ε��� �� �����ϴ�: {imagePath}");
        }

        return icon;  // �ε�� ������ ��ȯ
    }
}
