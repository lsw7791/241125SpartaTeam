using UnityEngine;

public class UnitRoot : MonoBehaviour
{
    [SerializeField] private TopDownAimRotation aimRotation;  // �� ȸ�� ��ũ��Ʈ ����

    // "OnAim" �޼��带 SendMessage�� ȣ���ϸ� �� �޼��尡 ����˴ϴ�.
    private void OnAim(Vector2 direction)
    {
        aimRotation.RotateArm(direction);  // �� ȸ�� ó��
    }

    public void RotateUnitRoot(bool value)
    {
        if (value)
        {
            // ��ü�� ȸ��: 180�� ȸ��
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            // ��ü�� ȸ��: 0�� ȸ��
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}