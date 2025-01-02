using System.Collections.Generic;
using MainData;
using UnityEngine;

public enum QuestAction
{
    Move,
    Action,
    Inventory,
    Status,
    Option,
    Attack,
    Craft
}

public class MainQuest : MainQuestData
{
    public int CurrentQuestId = 1;
    public Dictionary<int, bool> QuestCompletionStatus { get; private set; } = new Dictionary<int, bool>();
    public delegate void QuestUpdated(int questId);
    public event QuestUpdated OnQuestUpdated;

    public MainQuest()
    {
        // 초기 퀘스트 완료 상태 설정
        for (int i = 1; i <= 7; i++)
        {
            QuestCompletionStatus[i] = false;
        }
    }

    public MainQuestData GetData(int id)
    {
        return MainQuestDataMap[id];
    }
    public void Start()
    {
        GameManager.Instance.Player.PlayerInput.OnQuestActionTriggered += HandleQuestAction;
    }

    public void OnDestroy()
    {
        GameManager.Instance.Player.PlayerInput.OnQuestActionTriggered -= HandleQuestAction;
    }

    public bool IsQuestCompleted(int questId)
    {
        return QuestCompletionStatus.ContainsKey(questId) && QuestCompletionStatus[questId];
    }

    private void HandleQuestAction(QuestAction action)
    {
        if (CurrentQuestId > 7) return;

        switch (CurrentQuestId)
        {
            case 1 when action == QuestAction.Move:
            case 2 when action == QuestAction.Craft:
            case 3 when action == QuestAction.Action:
            case 4 when action == QuestAction.Inventory:
            case 5 when action == QuestAction.Status:
            case 6 when action == QuestAction.Option:
            case 7 when action == QuestAction.Attack:
                CompleteQuest(CurrentQuestId);
                break;
        }
    }

    public void CompleteQuest(int questId)
    {
        Debug.Log($"퀘스트 {questId} 완료!");
        QuestCompletionStatus[questId] = true;
        //CurrentQuestId++;
        OnQuestUpdated?.Invoke(CurrentQuestId); // UI 갱신 호출

        if (CurrentQuestId > 8)
        {
            Debug.Log("모든 필수 퀘스트 완료!");
        }
        else
        {
            StartQuest(CurrentQuestId);
        }
    }

    public void StartQuest(int questId)
    {
        Debug.Log($"퀘스트 {questId} 시작!");
        OnQuestUpdated?.Invoke(questId); // UI 갱신 호출
    }
}
