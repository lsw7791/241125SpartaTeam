using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainQuestUI : UIBase
{
    [SerializeField] private TMP_Text _questName;
    [SerializeField] private TMP_Text _questDesc;
    [SerializeField] private Button _completeBtn;

    private MainQuest _mainQuest;
    private int _currentQuestId;

    void OnEnable()
    {
        var mainQuest = GameManager.Instance.DataManager.MainQuest;
        if (mainQuest == null || GameManager.Instance.DataManager.MainQuest == null)
        {
            Debug.LogError("QuestManager �Ǵ� MainQuest�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return; // �ʱ�ȭ���� ���� ���¿��� ���� �ߴ�
        }

        _mainQuest = GameManager.Instance.DataManager.MainQuest;
        _currentQuestId = _mainQuest.CurrentQuestId;

        _mainQuest.OnQuestUpdated += UpdateUI; // ����Ʈ ������Ʈ �̺�Ʈ ����
        UpdateUI(_mainQuest.CurrentQuestId);   // �ʱ� UI ����

        _completeBtn.onClick.AddListener(OnCompleteButtonPressed);
    }


    void OnDestroy()
    {
        if (_mainQuest != null)
        {
            _mainQuest.OnQuestUpdated -= UpdateUI; // �̺�Ʈ ���� ����
        }
    }

    private void UpdateUI(int questId)
    {
        _currentQuestId = questId;

        // ����Ʈ ������ ��������
        var questData = _mainQuest.GetData(questId);

        if (questData != null)
        {
            _questName.text = questData.name;
            _questDesc.text = questData.desc;
        }
        else
        {
            _questName.text = "�� �� ���� ����Ʈ";
            _questDesc.text = "�����͸� ã�� �� �����ϴ�.";
        }

        _completeBtn.interactable = _mainQuest.IsQuestCompleted(questId);
    }

    private void OnCompleteButtonPressed()
    {
        if (_mainQuest.IsQuestCompleted(_currentQuestId))
        {
            _mainQuest.CompleteQuest(_currentQuestId); // ����Ʈ ����
        }
    }
}
