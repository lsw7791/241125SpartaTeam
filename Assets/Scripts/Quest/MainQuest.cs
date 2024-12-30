using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;

public class MainQuest : MonoBehaviour
{
    [SerializeField] TMP_Text _questName;
    [SerializeField] TMP_Text _questDesc;
    [SerializeField] Button _completeBtn;
    private int _currentQuestId = 1; // �⺻������ 1�� ����Ʈ���� ����

    void Start()
    {
        _completeBtn.onClick.AddListener(OnQuestComplete); // ��ư Ŭ�� �� ����Ʈ �Ϸ�
        ShowQuest(_currentQuestId); // ù ��° ����Ʈ ǥ��
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
            Debug.LogError("����Ʈ �����Ͱ� �����ϴ�. ID: " + questId);
        }
    }

    void OnQuestComplete()
    {
        // ���� ����Ʈ �Ϸ� ��, ���� ����Ʈ�� �Ѿ
        _currentQuestId++;
        if(_currentQuestId == 6)
        {
            this.gameObject.SetActive(false);
        }
        ShowQuest(_currentQuestId);  // ���� ����Ʈ ǥ��

        // ����Ʈ ���� ���¸� ���� (��: PlayerPrefs �Ǵ� �����ͺ��̽�)
        PlayerPrefs.SetInt("CurrentQuestId", _currentQuestId);
        PlayerPrefs.Save();
    }
}
