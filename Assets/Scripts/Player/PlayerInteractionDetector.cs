using UnityEngine;

public class PlayerInteractionDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌이 발생했을 때, NPC만 처리
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
        // 충돌이 끝났을 때, NPC만 처리
        if (collision.collider.CompareTag("NPC"))
        {
            // 현재 상호작용 대상과 일치하는 경우에만 null로 설정
            if (GameManager.Instance.InteractableObject == collision.collider.GetComponent<IInteractable>())
            {
                GameManager.Instance.InteractableObject = null;
            }
        }
    }
}
