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

        _mainQuest.OnQuestUpdated += UpdateUI; // 퀘스트 업데이트 이벤트 연결
        UpdateUI(_mainQuest.CurrentQuestId);   // 초기 UI 설정

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
            _mainQuest.OnQuestUpdated -= UpdateUI; // 이벤트 연결 해제
        }
    }

    private void UpdateUI(int questId)
    {
        GameManager.Instance.DataManager.MainQuest.CurrentQuestId = questId;

        // 퀘스트 데이터 가져오기
        var questData = _mainQuest.GetData(questId);

        if (questData != null)
        {
            _questName.text = questData.name;
            _questDesc.text = questData.desc;
        }
        else
        {
            _questName.text = "알 수 없는 퀘스트";
            _questDesc.text = "데이터를 찾을 수 없습니다.";
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
