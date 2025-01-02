using UnityEngine;
using UnityEngine.UI;

public class QuestIcon : UIBase
{
    // Ŭ�� �� ����� �޼���
    public void OnQuestIconClick()
    {
        Debug.Log("Quest Icon Clicked!");  // Ŭ�� Ȯ���� ���� �α�
        this.gameObject.SetActive(false);  // Ŭ�� �� ������ ��Ȱ��ȭ
        UIManager.Instance.ToggleUI<MainQuestUI>();

    }
}
