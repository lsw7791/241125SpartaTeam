using Constants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, UIBase> _uiList = new();

    public static float ScreenWidth = 1920;
    public static float ScreenHeight = 1080;

    protected override void Awake()
    {
        base.Awake();
    }
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

        newCanvasObject.transform.parent = this.transform;

        var canvas = newCanvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1;

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
    {
        T ui = GetUI<T>();

        if (ui != null && !ui.gameObject.activeSelf)
        {
            ui.gameObject.SetActive(true); // 비활성화된 UI를 다시 활성화
        }

        ui.Open();
        return ui;
    }


    public T CloseUI<T>() where T : UIBase
    {
        if (IsExistUI<T>())
        {
            T ui = _uiList[typeof(T).Name] as T;

            if (ui != null && ui.gameObject.activeSelf)
            {
                ui.Close();

                // 추가: UI 비활성화 후 초기화 수행
                ui.gameObject.SetActive(false);
            }

            return ui;
        }

        return null;
    }


    public void CloseAllUIs()
    { // 활성화 되어있는 모든 PopupUI 비활성화
        foreach (var ui in _uiList.Values)
        {
            if (ui?.gameObject.activeSelf == true)
            {
                ui.Close();
            }
        }
    }

    public bool ActiveUI()
    { // 활성화된 UI가 하나라도 있는지 확인
        foreach (var ui in _uiList.Values)
        {
            if (ui?.gameObject.activeSelf == true)
            {
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        _uiList.Clear();
    }

    // UI 토글 통합
    public T ToggleUI<T>() where T : UIBase
    {
        if (IsExistUI<T>())
        {
            var ui = GetUI<T>();
            if (ui.gameObject.activeSelf)
            {
                ui.Close();
            }
            else
            {
                ui.Open();
            }

            return ui;
        }
        else
        {
            T ui = OpenUI<T>();
            return ui;
        }
    }
    public T SetSortingOrder<T>(int order) where T : UIBase
    {
        T ui = GetUI<T>();
        var canvas = ui.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = order;
        }
        return ui;
    }


}
