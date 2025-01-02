using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainQuestUI : UIBase
{
    [SerializeField] private TMP_Text _questName;
    [SerializeField] private TMP_Text _questDesc;
    [SerializeField] private Button _completeBtn;

    private MainQuest _mainQuest;

    void OnEnable()
    {
        var mainQuest = GameManager.Instance.DataManager.MainQuest;
        
        _mainQuest = GameManager.Instance.DataManager.MainQuest;

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
        GameManager.Instance.DataManager.MainQuest.CurrentQuestId = questId;

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
        GameManager.Instance.DataManager.MainQuest.CurrentQuestId++;
        Debug.Log(GameManager.Instance.DataManager.MainQuest.CurrentQuestId);

        if (GameManager.Instance.DataManager.MainQuest.CurrentQuestId > 9)
        {
            Destroy(gameObject); 
        }

        UpdateUI(GameManager.Instance.DataManager.MainQuest.CurrentQuestId);
    }
}
