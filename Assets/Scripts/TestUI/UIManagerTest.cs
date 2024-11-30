using System.Collections.Generic;
using Tripolygon.UModelerX.Runtime.MessagePack.Formatters;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTest : MonoSingleton<UIManagerTest>
{
    [SerializeField] private Transform canvas;

    public static float ScreenWidth = 1920;
    public static float ScreenHeight = 1080;

    private Dictionary<string, UIBaseTest> uiList = new Dictionary<string, UIBaseTest>();

    public T Show<T>() where T : UIBaseTest
    {
        RemoveNull();

        string uiName = typeof(T).ToString();
        uiList.TryGetValue(uiName, out UIBaseTest ui);

        if (ui == null)
        {
            uiList.Remove(uiName);
            var uiObject = Resources.Load<UIBaseTest>($"Prefabs/UITest/{uiName}");

            ui = Load<T>(uiObject, uiName);
            uiList.Add(uiName, ui);
        }

        ui.gameObject.SetActive(true);
        return (T)ui;
    }

    private T Load<T>(UIBaseTest prefab, string uiName) where T : UIBaseTest
    {
        var newCanvasObject = new GameObject($"{uiName} Canvas");

        //newCanvasObject.transform.SetParent(transform);

        var canvas = newCanvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var canvasScaler = newCanvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(ScreenWidth, ScreenHeight);

        newCanvasObject.gameObject.AddComponent<GraphicRaycaster>();

        var ui = Instantiate(prefab, newCanvasObject.transform);
        ui.name = ui.name.Replace("(Clone)", "");

        var result = ui.GetComponent<T>();
        result.canvas = canvas;
        result.canvas.sortingOrder = uiList.Count;

        return result;
    }

    public T Get<T>() where T : UIBaseTest
    {
        string uiName = typeof(T).ToString();
        uiList.TryGetValue(uiName, out UIBaseTest ui);

        if(ui == null)
        {
            Debug.LogError($"{uiName} don't exist");
            return default;
        }

        return (T)ui;
    }

    public void Hide<T>()
    {
        string uiName = typeof(T).ToString();
        Hide(uiName);
    }

    public void Hide(string uiName)
    {
        uiList.TryGetValue(uiName, out UIBaseTest ui);

        if(ui == null)
        {
            return;
        }

        DestroyImmediate(ui.canvas.gameObject);
        uiList.Remove(uiName);
    }

    private void RemoveNull()
    { // 리스트에 null이 있는지 확인
        List<string> tempList = new List<string>(uiList.Count);

        foreach (var temp in uiList)
        {
            if (temp.Value == null)
            {
                tempList.Add(temp.Key);
            }
        }

        foreach (var temp in tempList)
        {
            uiList.Remove(temp);
        }
    }
}
