using UnityEngine;

public class Minimap : MonoBehaviour
{
    public RectTransform playerIcon;  // 미니맵에서 플레이어 아이콘
    public Transform player;          // 플레이어의 Transform

    public float minimapWidth = 200f; // 미니맵의 너비
    public float minimapHeight = 200f; // 미니맵의 높이
    public float mapWidth = 100f;      // 실제 월드의 가로 길이
    public float mapHeight = 100f;     // 실제 월드의 세로 길이

    void Update()
    {
        Vector3 playerPosition = player.position;

        // 월드 좌표를 미니맵 좌표로 변환 (상대적인 비율로)
        float xPos = Mathf.InverseLerp(-mapWidth / 2, mapWidth / 2, playerPosition.x) * minimapWidth;

        // 미니맵에서 반전된 y좌표 계산 (yPos)
        float yPos = Mathf.InverseLerp(-mapHeight / 2, mapHeight / 2, playerPosition.y) * minimapHeight;

        // yPos를 미니맵 좌표에서 반전시켜야 하므로, 아래쪽으로 이동하려면 반전시켜야 함
        yPos = minimapHeight - yPos;

        // 미니맵 좌표에 플레이어 아이콘 위치 설정
        playerIcon.anchoredPosition = new Vector2(xPos, -yPos);
    }
}