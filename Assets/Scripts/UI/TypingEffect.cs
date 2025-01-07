using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TypingEffect : MonoBehaviour
{
    public Text textComponent;  // 타이핑 효과를 적용할 Text 컴포넌트
    public string fullText;  // 출력할 전체 텍스트
    public float typingDuration = 3f;  // 텍스트가 타이핑되는 총 시간
    public Vector3 scaleFactor;  // 스케일 변화 크기
    public float scaleDuration = 1f;  // 스케일링 애니메이션의 지속 시간

    // Start is called before the first frame update
    void Start()
    {
        scaleFactor = new Vector3(1.05f, 1.05f, 1);
        fullText = "마일스톤 :\r\n대장장이의 길\r";
        // 타이핑 효과 시작
        StartTypingEffect();
    }

    private void StartTypingEffect()
    {
        // DOTween을 사용해 타이핑 효과 적용
        textComponent.DOText(fullText, typingDuration)
                     .SetEase(Ease.Linear)  // 텍스트 타이핑이 일정한 속도로 진행되도록 설정
                     .OnKill(StartScaling);  // 타이핑이 끝나면 StartScaling 호출
    }

    private void StartScaling()
    {
        // 타이핑 효과가 끝난 후 스케일링 애니메이션 시작
        transform.DOScale(scaleFactor, scaleDuration)  // 크기 변경 애니메이션
                 .SetLoops(-1, LoopType.Yoyo)          // Yoyo는 애니메이션을 반복하면서, 커졌다가 작아지는 효과
                 .SetEase(Ease.InOutSine);            // 부드러운 easing 효과
    }
}
