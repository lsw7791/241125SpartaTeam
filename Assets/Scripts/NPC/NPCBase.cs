
using UnityEngine;

public interface IInteractable
{
    public void Interact();
}
public abstract class NPCBase : MonoBehaviour, IInteractable
{
    public string npcName; // NPC �̸�
    public string[] defaultDialogue; // �⺻ ���

    // �߻� �޼���: �� NPC�� ������ ��ȣ�ۿ� ������ ����
    public abstract void Interact() ;

    // ���� �޼���: ��� NPC�� �����ϴ� ��ȭ ���
    protected void StartDialogue(string[] dialogueLines)
    {
        Debug.Log($"Starting dialogue with {npcName}");
    }
}
