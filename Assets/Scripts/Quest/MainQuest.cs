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
        // �ʱ� ����Ʈ �Ϸ� ���� ����
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
        Debug.Log($"����Ʈ {questId} �Ϸ�!");
        QuestCompletionStatus[questId] = true;
        //CurrentQuestId++;
        OnQuestUpdated?.Invoke(CurrentQuestId); // UI ���� ȣ��

        if (CurrentQuestId > 8)
        {
            Debug.Log("��� �ʼ� ����Ʈ �Ϸ�!");
        }
        else
        {
            StartQuest(CurrentQuestId);
        }
    }

    public void StartQuest(int questId)
    {
        Debug.Log($"����Ʈ {questId} ����!");
        OnQuestUpdated?.Invoke(questId); // UI ���� ȣ��
    }
}
