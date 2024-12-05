using UnityEngine;

public class Minimap : MonoBehaviour
{
    public RectTransform playerIcon;  // �̴ϸʿ��� �÷��̾� ������
    public Transform player;          // �÷��̾��� Transform

    public float minimapWidth = 200f; // �̴ϸ��� �ʺ�
    public float minimapHeight = 200f; // �̴ϸ��� ����
    public float mapWidth = 100f;      // ���� ������ ���� ����
    public float mapHeight = 100f;     // ���� ������ ���� ����

    void Update()
    {
        Vector3 playerPosition = player.position;

        // ���� ��ǥ�� �̴ϸ� ��ǥ�� ��ȯ (������� ������)
        float xPos = Mathf.InverseLerp(-mapWidth / 2, mapWidth / 2, playerPosition.x) * minimapWidth;

        // �̴ϸʿ��� ������ y��ǥ ��� (yPos)
        float yPos = Mathf.InverseLerp(-mapHeight / 2, mapHeight / 2, playerPosition.y) * minimapHeight;

        // yPos�� �̴ϸ� ��ǥ���� �������Ѿ� �ϹǷ�, �Ʒ������� �̵��Ϸ��� �������Ѿ� ��
        yPos = minimapHeight - yPos;

        // �̴ϸ� ��ǥ�� �÷��̾� ������ ��ġ ����
        playerIcon.anchoredPosition = new Vector2(xPos, -yPos);
    }
}