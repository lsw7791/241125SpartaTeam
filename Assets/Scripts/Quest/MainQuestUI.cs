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

        _mainQuest.OnQuestUpdated += UpdateUI; // 퀘스트 업데이트 이벤트 연결
        UpdateUI(_mainQuest.CurrentQuestId);   // 초기 UI 설정

        _completeBtn.onClick.AddListener(OnCompleteButtonPressed);
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
        GameManager.Instance.DataManager.MainQuest.CurrentQuestId++;
        Debug.Log(GameManager.Instance.DataManager.MainQuest.CurrentQuestId);

        if (GameManager.Instance.DataManager.MainQuest.CurrentQuestId > 9)
        {
            Destroy(gameObject); 
        }

        UpdateUI(GameManager.Instance.DataManager.MainQuest.CurrentQuestId);
    }
}
