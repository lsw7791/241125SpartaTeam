using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : SceneData
{
    public SceneData GetData(int id)
    {
        return SceneDataMap[id];
    }

    public List<SceneData> GetDataByType()
    {
        //return SceneDataList.FindAll(x=> x.type == 1)
        return null;
    }
    public string GetMapFrom(int id)
    {
        return SceneDataMap[id].mapFrom;
    }
    public string GetMapTo(int id)
    {
        return SceneDataMap[id].mapTo;
    }
    public Vector2 GetMoveTransform(int id)
    {
        Vector2 postion = new Vector2(SceneDataMap[id].xTransform, SceneDataMap[id].yTransform);
        return postion;
    }
}
