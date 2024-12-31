using UnityEngine;
using UnityEngine.UI;

public class QuestIcon : MonoBehaviour
{
    // 클릭 시 실행될 메서드
    public void OnQuestIconClick()
    {
        Debug.Log("Quest Icon Clicked!");  // 클릭 확인을 위한 로그
        this.gameObject.SetActive(false);  // 클릭 시 아이콘 비활성화
        UIManager.Instance.ToggleUI<MainQuestUI>();

    }
}
