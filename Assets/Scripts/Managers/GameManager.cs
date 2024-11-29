using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        DataManager.Instance.Initialize();

        ToolData testTool = DataManager.Instance.tool.GetItemData(1);
        Debug.Log(testTool.name);
    }
    private void Start()
    {
    }
}
