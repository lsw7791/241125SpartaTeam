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
    Craft,
    End,
    Info,
    MileStone
}

public class MainQuest : MainQuestData
{
    public int CurrentQuestId;
    public Dictionary<int, bool> QuestCompletionStatus { get; private set; } = new Dictionary<int, bool>();
    public delegate void QuestUpdated(int questId);
    public event QuestUpdated OnQuestUpdated;

    public MainQuest()
    {
        // 초기 퀘스트 완료 상태 설정
        for (int i = 0; i <= 8; i++)
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
        if (CurrentQuestId > 8) return;

        switch (CurrentQuestId)
        {
            case 0 when action == QuestAction.Move:
            case 1 when action == QuestAction.Info:
            case 2 when action == QuestAction.Craft:
            case 3 when action == QuestAction.Action:
            case 4 when action == QuestAction.Inventory:
            case 5 when action == QuestAction.Status:
            case 6 when action == QuestAction.Option:
            case 7 when action == QuestAction.Attack:
            case 8 when action == QuestAction.MileStone:
                break;
        }
    }

    public void CompleteQuest(int questId)
    {
        SoundManager.Instance.PlayClearSFX();
        QuestCompletionStatus[questId] = true;
        OnQuestUpdated?.Invoke(CurrentQuestId); // UI 갱신 호출

        if (CurrentQuestId > 8)
        {
           //
        }
        else
        {
            StartQuest(CurrentQuestId);
        }
    }

    public void StartQuest(int questId)
    {
        OnQuestUpdated?.Invoke(questId); // UI 갱신 호출
    }
}
