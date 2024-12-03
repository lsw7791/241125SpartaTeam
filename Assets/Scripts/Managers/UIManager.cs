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
        // dic 체크
        if (IsExistUI<T>()) // 있다 -> 반환
            return _uiList[uiName] as T;
        else // 없다 -> 새로 생성 반환
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
    { // null 체크 반환
        var uiName = typeof(T).Name;
        return _uiList.ContainsKey(uiName) && _uiList[uiName] != null;
    }

    public T OpenUI<T>() where T : UIBase
    { // 생성
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
    { // 활성화 되어있는 모든 PopupUI 비활성화
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
