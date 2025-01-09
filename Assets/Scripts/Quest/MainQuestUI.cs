using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainQuestUI : UIBase
{
    [SerializeField] private TMP_Text _questName;
    [SerializeField] private TMP_Text _questDesc;
    [SerializeField] private Button _completeBtn;

    private MainQuest _mainQuest;

    protected override void Awake()
    {
        base.Awake();
        var mainQuest = GameManager.Instance.DataManager.MainQuest;

        _mainQuest = GameManager.Instance.DataManager.MainQuest;

        _mainQuest.OnQuestUpdated += UpdateUI; // ����Ʈ ������Ʈ �̺�Ʈ ����
        UpdateUI(_mainQuest.CurrentQuestId);   // �ʱ� UI ����

        _completeBtn.onClick.AddListener(OnCompleteButtonPressed);
    }

    private void OnEnable()
    {
        UpdateUI(GameManager.Instance.Player.stats.CurrentQuestId);
        QuestIcon questicon = UIManager.Instance.GetUI<QuestIcon>();
        questicon.mainQuestUI = this;
        UIManager.Instance.CloseUI<QuestIcon>();
    }

    private void OnDisable()
    {
       // UIManager.Instance.ToggleUI<QuestIcon>();
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
        int reward = _mainQuest.GetData(GameManager.Instance.DataManager.MainQuest.CurrentQuestId).reward;
        GameManager.Instance.Player.stats.Gold += reward;
        GameManager.Instance.DataManager.MainQuest.CurrentQuestId++;
        GameManager.Instance.Player.stats.CurrentQuestId++;
        SoundManager.Instance.PlayClearSFX();

        if (GameManager.Instance.DataManager.MainQuest.CurrentQuestId > 8)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            UIManager.Instance.ToggleUI<EndingUI>();
        }

        UpdateUI(GameManager.Instance.DataManager.MainQuest.CurrentQuestId);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.inventory);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.stats);
        GameManager.Instance.Player.stats.nowMapNumber = GameManager.Instance.SceneNum;
    }
}
