using Unity.VisualScripting;
using UnityEngine;

public class UnitRoot : MonoBehaviour
{
    public void FlipRotation(Vector2 mouseWorldPos)
    {
        // ���콺 ��ġ�� �÷��̾��� ��ġ���� ���ʿ� ������ Y�� ȸ�� 180���� ����
        if (mouseWorldPos.x < transform.position.x)
        {
            // ������ �ٶ󺸰� (ȸ��)
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Y�� ȸ�� 180��
        }
        else
        {
            // �������� �ٶ󺸰� (ȸ�� ���)
            transform.rotation = Quaternion.Euler(0, 180, 0);    // �⺻ ȸ�� (Y�� 0��)
        }
    }
}