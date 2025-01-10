using UnityEngine;
using DG.Tweening;

public class QuestIcon : UIBase
{
    public float scaleDuration = 1f;  
    public float scaleFactor = 1.5f;

    public MainQuestUI mainQuestUI;


    public GameObject _questText;

    // Ŭ�� �� ����� �޼���
    public void OnQuestIconClick()
    {
        UIManager.Instance.ToggleUI<MainQuestUI>();
        UIManager.Instance.CloseUI<QuestIcon>();
        GameManager.Instance.Player.playerState = Player.PlayerState.Idle;
    }

    private void Start()
    {
        StartScaling();
    }

    private void OnEnable()
    {
        _questText.SetActive(false);
    }

    private void StartScaling()
    {
        transform.DOScale(scaleFactor, scaleDuration)  // ũ�� ���� �ִϸ��̼�
                 .SetLoops(-1, LoopType.Yoyo)          // Yoyo�� �ִϸ��̼��� �ݺ��ϸ鼭, Ŀ���ٰ� �۾����� ȿ��
                 .SetEase(Ease.InOutSine);            // �ε巯�� easing ȿ��
    }
}
