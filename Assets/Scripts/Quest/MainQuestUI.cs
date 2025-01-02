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
            Debug.LogError("QuestManager 또는 MainQuest가 초기화되지 않았습니다.");
            return; // 초기화되지 않은 상태에서 실행 중단
        }

        _mainQuest = GameManager.Instance.DataManager.MainQuest;
        _currentQuestId = _mainQuest.CurrentQuestId;

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
        _currentQuestId = questId;

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
        if (_mainQuest.IsQuestCompleted(_currentQuestId))
        {
            _mainQuest.CompleteQuest(_currentQuestId); // 퀘스트 진행
        }
    }
}
