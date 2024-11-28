using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    public void RotateArm(Vector2 direction)
    {
        // ���� ȸ�� ���
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);  // ȸ�� ����
    }
}