using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;

public class MainQuest : MonoBehaviour
{
    [SerializeField] TMP_Text _questName;
    [SerializeField] TMP_Text _questDesc;
    [SerializeField] Button _completeBtn;
    private int currentQuestId = 1; // �⺻������ 1�� ����Ʈ���� ����

    void Start()
    {
        _completeBtn.onClick.AddListener(OnQuestComplete); // ��ư Ŭ�� �� ����Ʈ �Ϸ�
        ShowQuest(currentQuestId); // ù ��° ����Ʈ ǥ��
    }

    void ShowQuest(int questId)
    {
        MainQuestManager questManager = GameManager.Instance.DataManager.MainQuest; // ��η� ����
        MainQuestData questData = questManager.GetData(questId);

        if (questData != null)
        {
            _questName.text = questData.name;
            _questDesc.text = questData.desc;
        }
    }

    void OnQuestComplete()
    {
        // ���� ����Ʈ �Ϸ� ��, ���� ����Ʈ�� �Ѿ
        currentQuestId++;
        ShowQuest(currentQuestId);  // ���� ����Ʈ ǥ��

        // ����Ʈ ���� ���¸� ���� (��: PlayerPrefs �Ǵ� �����ͺ��̽�)
        PlayerPrefs.SetInt("CurrentQuestId", currentQuestId);
        PlayerPrefs.Save();
    }

    void LoadQuestProgress()
    {
        // ����� ����Ʈ ���� ���¸� �ҷ�����
        if (PlayerPrefs.HasKey("CurrentQuestId"))
        {
            currentQuestId = PlayerPrefs.GetInt("CurrentQuestId");
            ShowQuest(currentQuestId);
        }
        else
        {
            currentQuestId = 1; // �⺻���� ù ��° ����Ʈ
            ShowQuest(currentQuestId);
        }
    }
}
