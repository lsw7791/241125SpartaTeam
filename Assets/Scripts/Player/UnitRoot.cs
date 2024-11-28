using UnityEngine;

public class UnitRoot : MonoBehaviour
{
    [SerializeField] private TopDownAimRotation aimRotation;  // 팔 회전 스크립트 참조

    // "OnAim" 메서드를 SendMessage로 호출하면 이 메서드가 실행됩니다.
    private void OnAim(Vector2 direction)
    {
        aimRotation.RotateArm(direction);  // 팔 회전 처리
    }

    public void RotateUnitRoot(bool value)
    {
        if (value)
        {
            // 본체의 회전: 180° 회전
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            // 본체의 회전: 0° 회전
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}