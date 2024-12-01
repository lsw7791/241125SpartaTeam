using Constants;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerTest : MonoSingleton<UIManagerTest>
{
    private Dictionary<string, UIBaseTest> _uiList = new Dictionary<string, UIBaseTest>();

    public T GetUI<T>() where T : UIBaseTest
    {
        var uiName = typeof(T).Name;

        if (_uiList.TryGetValue(uiName, out var value) && value != null)
            return value as T;
        else
            return CreateUI<T>();
    }

    private T CreateUI<T>() where T : UIBaseTest
    {
        var uiName = typeof(T).Name;

        T uiRes = Resources.Load<T>($"{PathInfo.UIPath}{uiName}");

        var outUi = Instantiate(uiRes);
        outUi.name = outUi.name.Replace("(Clone)", "");

        return outUi;
    }

    public bool IsExistUI<T>() where T : UIBaseTest
    {
        var uiName = typeof(T).Name;
        return _uiList.ContainsKey(uiName) && _uiList[uiName] != null;
    }

    public T OpenUI<T>() where T : UIBaseTest
    {
        T ui = GetUI<T>();
        ui.Open();
        return ui;
    }

    public T CloseUI<T>() where T : UIBaseTest
    {
        T ui = GetUI<T>();
        ui.Close();

        return ui;
    }

    public void Clear()
    {
        _uiList.Clear();
    }
}
