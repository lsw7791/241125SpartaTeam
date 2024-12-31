using MainData;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MainQuestData
{
    public MainQuest MainQuest { get; private set; }

    void Awake()
    {
        MainQuest = new MainQuest();
        MainQuest.Start();
    }

    public MainQuestData GetData(int id)
    {
        return MainQuestDataMap[id];
    }
}
