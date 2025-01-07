using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  

public class QuestIcon : UIBase
{
    public float scaleDuration = 1f;  
    public float scaleFactor = 1.5f;  
    // Ŭ�� �� ����� �޼���
    public void OnQuestIconClick()
    {
        this.gameObject.SetActive(false);  
        UIManager.Instance.ToggleUI<MainQuestUI>();
    }

    private void Start()
    {
        StartScaling();
    }

    private void StartScaling()
    {
        transform.DOScale(scaleFactor, scaleDuration)  // ũ�� ���� �ִϸ��̼�
                 .SetLoops(-1, LoopType.Yoyo)          // Yoyo�� �ִϸ��̼��� �ݺ��ϸ鼭, Ŀ���ٰ� �۾����� ȿ��
                 .SetEase(Ease.InOutSine);            // �ε巯�� easing ȿ��
    }
}
