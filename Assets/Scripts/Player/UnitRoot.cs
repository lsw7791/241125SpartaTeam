using Unity.VisualScripting;
using UnityEngine;

public class UnitRoot : MonoBehaviour
{
    public void FlipRotation(Vector2 mouseWorldPos)
    {
        // 마우스 위치가 플레이어의 위치보다 왼쪽에 있으면 Y축 회전 180도로 설정
        if (mouseWorldPos.x < transform.position.x)
        {
            // 왼쪽을 바라보게 (회전)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Y축 회전 180도
        }
        else
        {
            // 오른쪽을 바라보게 (회전 취소)
            transform.rotation = Quaternion.Euler(0, 180, 0);    // 기본 회전 (Y축 0도)
        }
    }
}