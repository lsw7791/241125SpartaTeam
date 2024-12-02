using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerTest : MonoSingleton<UIManagerTest>
{
    private Dictionary<string, UIBaseTest> _uiList = new();

    public static float ScreenWidth = 1920;
    public static float ScreenHeight = 1080;

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

        var newCanvasObject = new GameObject($"{uiName} Canvas");

        var canvas = newCanvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var canvasScaler = newCanvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(ScreenWidth, ScreenHeight);

        newCanvasObject.gameObject.AddComponent<GraphicRaycaster>();

        T uiRes = Resources.Load<T>($"{PathInfo.UIPath}{uiName}");

        var outUi = Instantiate(uiRes, newCanvasObject.transform);
        outUi.name = outUi.name.Replace("(Clone)", "");
        //outUi.canvas = canvas;
        //outUi.canvas.sortingOrder = _uiList.Count;

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

    public T CloseUI<T>() where T : UIBaseTest
    {
        T ui = GetUI<T>();
        ui.Close();

        return ui;
    }

    //public void Hide<T>()
    //{
    //    string uiName = typeof(T).ToString();
    //    Hide(uiName);
    //}

    //public void Hide(string uiName)
    //{
    //    _uiList.TryGetValue(uiName, out UIBaseTest ui);

    //    if (ui == null)
    //    {
    //        return;
    //    }

    //    DestroyImmediate(ui.canvas.gameObject);
    //    _uiList.Remove(uiName);
    //}

    public void Clear()
    { // �̰ɷ� ��� Ui�� ���������� Ȯ�� �ʿ�
        _uiList.Clear();
    }

    //public void Init()
    //{ // �� �Ѿ �� UIList �ʱ�ȭ
    //    SceneManager.sceneLoaded += (scene, mode) => { _uiList.Clear(); };
    //}
}
