using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TypingEffect : MonoBehaviour
{
    public Text textComponent;  // Ÿ���� ȿ���� ������ Text ������Ʈ
    public string fullText;  // ����� ��ü �ؽ�Ʈ
    public float typingDuration = 3f;  // �ؽ�Ʈ�� Ÿ���εǴ� �� �ð�
    public Vector3 scaleFactor;  // ������ ��ȭ ũ��
    public float scaleDuration = 1f;  // �����ϸ� �ִϸ��̼��� ���� �ð�

    // Start is called before the first frame update
    void Start()
    {
        scaleFactor = new Vector3(1.05f, 1.05f, 1);
        fullText = "���Ͻ��� :\r\n���������� ��\r";
        // Ÿ���� ȿ�� ����
        StartTypingEffect();
    }

    private void StartTypingEffect()
    {
        // DOTween�� ����� Ÿ���� ȿ�� ����
        textComponent.DOText(fullText, typingDuration)
                     .SetEase(Ease.Linear)  // �ؽ�Ʈ Ÿ������ ������ �ӵ��� ����ǵ��� ����
                     .OnKill(StartScaling);  // Ÿ������ ������ StartScaling ȣ��
    }

    private void StartScaling()
    {
        // Ÿ���� ȿ���� ���� �� �����ϸ� �ִϸ��̼� ����
        transform.DOScale(scaleFactor, scaleDuration)  // ũ�� ���� �ִϸ��̼�
                 .SetLoops(-1, LoopType.Yoyo)          // Yoyo�� �ִϸ��̼��� �ݺ��ϸ鼭, Ŀ���ٰ� �۾����� ȿ��
                 .SetEase(Ease.InOutSine);            // �ε巯�� easing ȿ��
    }
}
