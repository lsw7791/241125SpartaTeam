using MainData;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestManager :MainQuestData
{

    // 퀘스트 데이터를 받아오는 메서드
    public MainQuestData GetData(int id)
    {
            return MainQuestDataMap[id];
    }
}
