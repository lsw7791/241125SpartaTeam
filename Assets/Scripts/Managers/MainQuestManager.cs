using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestManager : MainQuestData
{
    public MainQuestData GetData(int id)
    {
        return MainQuestDataMap[id];
    }
}
