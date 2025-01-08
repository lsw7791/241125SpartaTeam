using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  

public class QuestIcon : UIBase
{
    public float scaleDuration = 1f;  
    public float scaleFactor = 1.5f;

    public MainQuestUI mainQuestUI;

    // 클릭 시 실행될 메서드
    public void OnQuestIconClick()
    {
        UIManager.Instance.ToggleUI<MainQuestUI>();
        UIManager.Instance.CloseUI<QuestIcon>();
    }

    private void Start()
    {
        StartScaling();
    }

    private void StartScaling()
    {
        transform.DOScale(scaleFactor, scaleDuration)  // 크기 변경 애니메이션
                 .SetLoops(-1, LoopType.Yoyo)          // Yoyo는 애니메이션을 반복하면서, 커졌다가 작아지는 효과
                 .SetEase(Ease.InOutSine);            // 부드러운 easing 효과
    }
}
