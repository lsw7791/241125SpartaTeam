using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;

public class MainQuest : MonoBehaviour
{
    [SerializeField] TMP_Text _questName;
    [SerializeField] TMP_Text _questDesc;
    [SerializeField] Button _completeBtn;
    private int currentQuestId = 1; // 기본적으로 1번 퀘스트부터 시작

    void Start()
    {
        _completeBtn.onClick.AddListener(OnQuestComplete); // 버튼 클릭 시 퀘스트 완료
        ShowQuest(currentQuestId); // 첫 번째 퀘스트 표시
    }

    void ShowQuest(int questId)
    {
        MainQuestManager questManager = GameManager.Instance.DataManager.MainQuest; // 경로로 접근
        MainQuestData questData = questManager.GetData(questId);

        if (questData != null)
        {
            _questName.text = questData.name;
            _questDesc.text = questData.desc;
        }
    }

    void OnQuestComplete()
    {
        // 현재 퀘스트 완료 후, 다음 퀘스트로 넘어감
        currentQuestId++;
        ShowQuest(currentQuestId);  // 다음 퀘스트 표시

        // 퀘스트 진행 상태를 저장 (예: PlayerPrefs 또는 데이터베이스)
        PlayerPrefs.SetInt("CurrentQuestId", currentQuestId);
        PlayerPrefs.Save();
    }

    void LoadQuestProgress()
    {
        // 저장된 퀘스트 진행 상태를 불러오기
        if (PlayerPrefs.HasKey("CurrentQuestId"))
        {
            currentQuestId = PlayerPrefs.GetInt("CurrentQuestId");
            ShowQuest(currentQuestId);
        }
        else
        {
            currentQuestId = 1; // 기본값은 첫 번째 퀘스트
            ShowQuest(currentQuestId);
        }
    }
}
