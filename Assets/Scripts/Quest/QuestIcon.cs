using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    // Ŭ�� �� ����� �޼���
    public void OnQuestIconClick()
    {
        Debug.Log("Quest Icon Clicked!");  // Ŭ�� Ȯ���� ���� �α�
        this.gameObject.SetActive(false);  // Ŭ�� �� ������ ��Ȱ��ȭ
    }
}
