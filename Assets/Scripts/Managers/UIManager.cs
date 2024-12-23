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
    { // null üũ ��ȯ
        var uiName = typeof(T).Name;
        return _uiList.ContainsKey(uiName) && _uiList[uiName] != null;
    }

    public T OpenUI<T>() where T : UIBase
    {
        T ui = GetUI<T>();

        if (ui != null && !ui.gameObject.activeSelf)
        {
            ui.gameObject.SetActive(true); // ��Ȱ��ȭ�� UI�� �ٽ� Ȱ��ȭ
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

                // �߰�: UI ��Ȱ��ȭ �� �ʱ�ȭ ����
                ui.gameObject.SetActive(false);
            }

            return ui;
        }

        return null;
    }


    public void CloseAllUIs()
    { // Ȱ��ȭ �Ǿ��ִ� ��� PopupUI ��Ȱ��ȭ
        foreach (var ui in _uiList.Values)
        {
            if (ui?.gameObject.activeSelf == true)
            {
                ui.Close();
            }
        }
    }

    public bool ActiveUI()
    { // Ȱ��ȭ�� UI�� �ϳ��� �ִ��� Ȯ��
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

    // UI ��� ����
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
