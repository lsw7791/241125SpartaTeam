using Constants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerTest : MonoSingleton<UIManagerTest>
{
    private Dictionary<string, UIBaseTest> _uiList = new();

    public T GetUI<T>() where T : UIBaseTest
    {
        var uiName = typeof(T).Name;
        // dic üũ
        if (IsExistUI<T>()) // �ִ� -> ��ȯ
            return _uiList[uiName] as T;
        else // ���� -> ���� ���� ��ȯ
            return CreateUI<T>();
    }

    private T CreateUI<T>() where T : UIBaseTest
    {
        var uiName = typeof(T).Name;

        T uiRes = Resources.Load<T>($"{PathInfo.UIPath}{uiName}");

        var outUi = Instantiate(uiRes);
        outUi.name = outUi.name.Replace("(Clone)", "");

        if (IsExistUI<T>())
            _uiList[uiName] = outUi;
        else
            _uiList.Add(uiName, outUi);

        return outUi;
    }

    public bool IsExistUI<T>() where T : UIBaseTest
    { // null üũ ��ȯ
        var uiName = typeof(T).Name;
        return _uiList.ContainsKey(uiName) && _uiList[uiName] != null;
    }

    public T OpenUI<T>() where T : UIBaseTest
    { // ����
        T ui = GetUI<T>();
        ui.Open();
        return ui;
    }

    //public T CloseUI<T>() where T : UIBaseTest
    //{
    //    T ui = GetUI<T>();
    //    ui.Close();

    //    return ui;
    //}

    public void Clear()
    {
        _uiList.Clear();
    }

    public void Init()
    { // �� �Ѿ �� UIList �ʱ�ȭ
        SceneManager.sceneLoaded += (scene, mode) => { _uiList.Clear(); };
    }
}
