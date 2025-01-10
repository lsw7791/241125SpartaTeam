using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Dictionary<string, UIBase> UiList = new(); // UI ��ü ����Ʈ
    private Dictionary<string, Canvas> _uiCanvases = new(); // UI ĵ���� ����Ʈ
    private int _sortingOrderCounter = 0; // sortingOrder�� ������ ī����
    public SpriteAtlas craftingAtlas;
    public SpriteAtlas ItemAtlas;
    public SpriteAtlas UIAtlas;
    public SpriteAtlas BackgroundAtlas;
    public FadeManager fadeManager;
    public BrightnessUI brightnessUI;
    protected override void Awake()
    {
        base.Awake();
        fadeManager = GetComponentInChildren<FadeManager>();
        brightnessUI = GetComponentInChildren<BrightnessUI>();
    }

    // UI�� ��������, ���� �������� ������ ����
    public T GetUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;

        if (UiList.ContainsKey(uiName))
        {
            return UiList[uiName] as T;
        }
        else
        {
            return CreateUI<T>();
        }
    }

    // UI�� �����ϴ� �޼���
    private T CreateUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;

        // MainQuestUI�� ��� ����Ʈ�� �߰����� �ʰ�, �ٷ� ������ �ϰ� ��Ȱ��ȭ
        if (uiName == "MainQuestUI")
        {
            T uiPrefab = Resources.Load<T>($"{PathInfo.UIPath}{uiName}");

            if (uiPrefab == null)
            {
                Debug.LogError($"UI Prefab not found: {uiName}");
                return null;
            }

            // ���� ĵ���� ����
            var canvasObject = new GameObject($"{uiName}_Canvas");
            Canvas uiCanvas = canvasObject.AddComponent<Canvas>();
            uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // �ʱ� sortingOrder ���� (���� ī���� �� ���)
            uiCanvas.sortingOrder = _sortingOrderCounter;

            var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.matchWidthOrHeight = 1;

            canvasObject.AddComponent<GraphicRaycaster>();

            // UI �ν��Ͻ��� ĵ������ �߰�
            var uiInstance = Instantiate(uiPrefab, uiCanvas.transform);
            uiInstance.name = uiInstance.name.Replace("(Clone)", "");

            // UI�� ó�� ������ �� ��Ȱ��ȭ ���·� ����
            uiInstance.gameObject.SetActive(false);

            // UIManager�� �ڽ����� ĵ������ ����
            canvasObject.transform.SetParent(this.transform);

            // �ֽ� sortingOrder �� ����
            _sortingOrderCounter++;

            return uiInstance;
        }
        else
        {
            // MainQuestUI�� �ƴ� ��쿡�� UiList�� �߰��Ͽ� ����
            T uiPrefab = Resources.Load<T>($"{PathInfo.UIPath}{uiName}");

            if (uiPrefab == null)
            {
                Debug.LogError($"UI Prefab not found: {uiName}");
                return null;
            }

            // ���� ĵ���� ����
            var canvasObject = new GameObject($"{uiName}_Canvas");
            Canvas uiCanvas = canvasObject.AddComponent<Canvas>();
            uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // �ʱ� sortingOrder ���� (���� ī���� �� ���)
            uiCanvas.sortingOrder = _sortingOrderCounter;

            var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.matchWidthOrHeight = 1;

            canvasObject.AddComponent<GraphicRaycaster>();

            // UI �ν��Ͻ��� ĵ������ �߰�
            var uiInstance = Instantiate(uiPrefab, uiCanvas.transform);
            uiInstance.name = uiInstance.name.Replace("(Clone)", "");

            // UI�� ó�� ������ �� ��Ȱ��ȭ ���·� ����
            uiInstance.gameObject.SetActive(false);

            // UIManager�� �ڽ����� ĵ������ ����
            canvasObject.transform.SetParent(this.transform);

            // ������ UI�� �����ϴ� ����Ʈ�� �߰�
            UiList[uiName] = uiInstance;
            _uiCanvases[uiName] = uiCanvas;

            // �ֽ� sortingOrder �� ����
            _sortingOrderCounter++;

            return uiInstance;
        }
    }


    // UI�� ���� �޼���
    public T OpenUI<T>() where T : UIBase
    {
        T ui = GetUI<T>();

        // UiList�� UI�� ������ �ٷ� ����
        if (ui == null)
        {
            return null;
        }

        // UI�� ��Ȱ��ȭ�Ǿ� ������ Ȱ��ȭ
        if (!ui.gameObject.activeSelf)
        {
            ui.gameObject.SetActive(true);
        }

        // ĵ������ ��������
        if (_uiCanvases.ContainsKey(typeof(T).Name))
        {
            Canvas uiCanvas = _uiCanvases[typeof(T).Name].GetComponent<Canvas>();

            // ĵ������ UIManager�� �ڽ����� ����
            uiCanvas.transform.SetParent(this.transform, false); // ���� ��ǥ�� ������ ä�� �θ� ����

            // ���ο� sortingOrder ���� (���� ī���� �� ���)
            uiCanvas.sortingOrder = _sortingOrderCounter;
        }
       

        ui.Open(); // UI�� ���� �޼��� ȣ��
        return ui;
    }


    // UI�� �ݴ� �޼���
    public void CloseUI<T>() where T : UIBase
    {
        if (UiList.TryGetValue(typeof(T).Name, out var uiBase) && uiBase.gameObject.activeSelf)
        {
            uiBase.Close(); // UI�� �ݴ� �޼��� ȣ��
            uiBase.gameObject.SetActive(false); // UI�� ��Ȱ��ȭ
        }
    }

    // ��� UI�� �ݴ� �޼���
    public void CloseAllUIs()
    {
        foreach (var ui in UiList.Values)
        {
            if (ui.gameObject.activeSelf)
            {
                ui.Close();
                ui.gameObject.SetActive(false); // ��� UI�� ��Ȱ��ȭ
            }
        }
    }

    // UI�� ����ϴ� �޼���
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
            // �̹� UI�� ���������� sortingOrder�� ���� (�� ���� ����)
            Canvas uiCanvas = _uiCanvases[typeof(T).Name];
            uiCanvas.sortingOrder = _sortingOrderCounter;

            // sortingOrder ī���� ���� (�� ���� ����)
            _sortingOrderCounter++;

            return ui;
        }
        else
        {
            T ui = OpenUI<T>();
            return ui;
        }
            //Debug.Log(GameManager.Instance.Player.playerState);
    }

    // UI�� �����ϴ��� Ȯ���ϴ� �޼���
    public bool IsExistUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;
        return UiList.ContainsKey(uiName) && UiList[uiName] != null;
    }

    // Ȱ��ȭ�� UI�� �ִ��� Ȯ���ϴ� �޼���
    public bool IsActiveUI()
    {
        foreach (var ui in UiList.Values)
        {
            if (ui.gameObject.activeSelf)
            {
                return true; // �ϳ��� Ȱ��ȭ�� UI�� ������ true ��ȯ
            }
        }

        return false; // Ȱ��ȭ�� UI�� ������ false ��ȯ
    }

    public T ActiveUI<T>() where T : UIBase
    {
        if (!IsExistUI<T>())
        {
            return null;
        }

        T ui = GetUI<T>();

        return ui;
    }
    public IEnumerator DelayToggleQuestUI(float delayTime)
    {
        // ������ �ð�(��)��ŭ ��ٸ��ϴ�.
        yield return new WaitForSeconds(delayTime);

        // 3�� �� UI�� ����մϴ�.
        UIManager.Instance.ToggleUI<QuestIcon>();
    }
}
