using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;

public class MainQuest : MonoBehaviour
{
    [SerializeField] TMP_Text _questName;
    [SerializeField] TMP_Text _questDesc;
    [SerializeField] Button _completeBtn;
    private int _currentQuestId = 1; // 기본적으로 1번 퀘스트부터 시작

    void Start()
    {
        _completeBtn.onClick.AddListener(OnQuestComplete); // 버튼 클릭 시 퀘스트 완료
        ShowQuest(_currentQuestId); // 첫 번째 퀘스트 표시
    }

    void ShowQuest(int questId)
    {
        MainQuestManager questManager = GameManager.Instance.DataManager.MainQuest;
        MainQuestData questData = questManager.GetData(questId);

        if (questData != null)
        {
            _questName.text = questData.name;
            _questDesc.text = questData.desc;
        }
        else
        {
            Debug.LogError("퀘스트 데이터가 없습니다. ID: " + questId);
        }
    }

    void OnQuestComplete()
    {
        // 현재 퀘스트 완료 후, 다음 퀘스트로 넘어감
        _currentQuestId++;
        if(_currentQuestId == 6)
        {
            this.gameObject.SetActive(false);
        }
        ShowQuest(_currentQuestId);  // 다음 퀘스트 표시

        // 퀘스트 진행 상태를 저장 (예: PlayerPrefs 또는 데이터베이스)
        PlayerPrefs.SetInt("CurrentQuestId", _currentQuestId);
        PlayerPrefs.Save();
    }
}
