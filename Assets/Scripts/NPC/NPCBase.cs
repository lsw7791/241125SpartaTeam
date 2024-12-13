
using UnityEngine;

public interface IInteractable
{
    public void Interact();
}
public abstract class NPCBase : MonoBehaviour, IInteractable
{
    public string npcName; // NPC 이름
    public string[] defaultDialogue; // 기본 대사

    // 추상 메서드: 각 NPC가 고유한 상호작용 동작을 구현
    public abstract void Interact() ;

    // 공통 메서드: 모든 NPC가 공유하는 대화 기능
    protected void StartDialogue(string[] dialogueLines)
    {
        Debug.Log($"Starting dialogue with {npcName}");
    }
}
