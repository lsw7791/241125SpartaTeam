using MainData;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestManager :MainQuestData
{

    // ����Ʈ �����͸� �޾ƿ��� �޼���
    public MainQuestData GetData(int id)
    {
            return MainQuestDataMap[id];
    }
}
