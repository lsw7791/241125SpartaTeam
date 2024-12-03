using Constants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, UIBase> _uiList = new();

    public static float ScreenWidth = 1920;
    public static float ScreenHeight = 1080;

    public T GetUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;
        // dic üũ
        if (IsExistUI<T>()) // �ִ� -> ��ȯ
            return _uiList[uiName] as T;
        else // ���� -> ���� ���� ��ȯ
            return CreateUI<T>();
    }

    private T CreateUI<T>() where T : UIBase
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

        if (IsExistUI<T>())
            _uiList[uiName] = outUi;
        else
            _uiList.Add(uiName, outUi);

        return outUi;
    }

    public bool IsExistUI<T>() where T : UIBase
    { // null üũ ��ȯ
        var uiName = typeof(T).Name;
        return _uiList.ContainsKey(uiName) && _uiList[uiName] != null;
    }

    public T OpenUI<T>() where T : UIBase
    { // ����
        T ui = GetUI<T>();
        ui.Open();
        return ui;
    }

    public T CloseUI<T>() where T : UIBase
    {
        T ui = GetUI<T>();
        ui.Close();

        return ui;
    }

    public void CloseAllUIs()
    { // Ȱ��ȭ �Ǿ��ִ� ��� PopupUI ��Ȱ��ȭ
        foreach (var ui in _uiList.Values)
        {
            if (ui != null && ui.gameObject.activeSelf)
            {
                ui.Close();
            }
        }
    }

    public void Clear()
    {
        _uiList.Clear();
    }
}
