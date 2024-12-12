using UnityEngine;

public class PlayerInteractionDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� �߻����� ��, NPC�� ó��
        if (collision.collider.CompareTag("NPC"))
        {
            IInteractable interactable = collision.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                GameManager.Instance.InteractableObject = interactable;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ������ ��, NPC�� ó��
        if (collision.collider.CompareTag("NPC"))
        {
            // ���� ��ȣ�ۿ� ���� ��ġ�ϴ� ��쿡�� null�� ����
            if (GameManager.Instance.InteractableObject == collision.collider.GetComponent<IInteractable>())
            {
                GameManager.Instance.InteractableObject = null;
            }
        }
    }
}
