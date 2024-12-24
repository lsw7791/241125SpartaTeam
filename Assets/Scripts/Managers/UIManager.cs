using Constants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, UIBase> _uiList = new(); // UI 객체 리스트
    private Dictionary<string, Canvas> _uiCanvases = new(); // UI 캔버스 리스트
    private int _sortingOrderCounter = 0; // sortingOrder를 관리할 카운터

    protected override void Awake()
    {
        base.Awake();
    }

    // UI를 가져오고, 만약 존재하지 않으면 생성
    public T GetUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;

        if (_uiList.ContainsKey(uiName))
        {
            return _uiList[uiName] as T;
        }
        else
        {
            return CreateUI<T>();
        }
    }

    // UI를 생성하는 메서드
    private T CreateUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;
        T uiPrefab = Resources.Load<T>($"{PathInfo.UIPath}{uiName}");

        if (uiPrefab == null)
        {
            Debug.LogError($"UI Prefab not found: {uiName}");
            return null;
        }

        // 개별 캔버스 생성
        var canvasObject = new GameObject($"{uiName}_Canvas");
        Canvas uiCanvas = canvasObject.AddComponent<Canvas>();
        uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // 초기 sortingOrder 설정 (현재 카운터 값 사용)
        uiCanvas.sortingOrder = _sortingOrderCounter;

        var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);

        canvasObject.AddComponent<GraphicRaycaster>();

        // UI 인스턴스를 캔버스에 추가
        var uiInstance = Instantiate(uiPrefab, uiCanvas.transform);
        uiInstance.name = uiInstance.name.Replace("(Clone)", "");

        // UI를 처음 생성할 때 비활성화 상태로 설정
        uiInstance.gameObject.SetActive(false);

        // UIManager의 자식으로 캔버스를 설정
        canvasObject.transform.SetParent(this.transform);

        // 생성된 UI를 관리하는 리스트에 추가
        _uiList[uiName] = uiInstance;
        _uiCanvases[uiName] = uiCanvas;

        // 최신 sortingOrder 값 갱신
        _sortingOrderCounter++;

        return uiInstance;
    }

    // UI를 여는 메서드
    public T OpenUI<T>() where T : UIBase
    {
        T ui = GetUI<T>();

        if (ui != null && !ui.gameObject.activeSelf)
        {
            ui.gameObject.SetActive(true); // UI가 비활성화되어 있으면 활성화
        }

        // 캔버스 가져오기
        Canvas uiCanvas = _uiCanvases[typeof(T).Name].GetComponent<Canvas>();

        // 캔버스를 UIManager의 자식으로 설정
        uiCanvas.transform.SetParent(this.transform, false); // 로컬 좌표를 유지한 채로 부모를 변경

        // 새로운 sortingOrder 설정 (현재 카운터 값 사용)
        uiCanvas.sortingOrder = _sortingOrderCounter;

        ui.Open(); // UI를 여는 메서드 호출
        return ui;
    }

    // UI를 닫는 메서드
    public void CloseUI<T>() where T : UIBase
    {
        if (_uiList.TryGetValue(typeof(T).Name, out var uiBase) && uiBase.gameObject.activeSelf)
        {
            uiBase.Close(); // UI를 닫는 메서드 호출
            uiBase.gameObject.SetActive(false); // UI를 비활성화
        }
    }

    // 모든 UI를 닫는 메서드
    public void CloseAllUIs()
    {
        foreach (var ui in _uiList.Values)
        {
            if (ui.gameObject.activeSelf)
            {
                ui.Close();
                ui.gameObject.SetActive(false); // 모든 UI를 비활성화
            }
        }
    }

    // UI를 토글하는 메서드
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

            // 이미 UI가 열려있으면 sortingOrder를 갱신 (한 번만 증가)
            Canvas uiCanvas = _uiCanvases[typeof(T).Name];
            uiCanvas.sortingOrder = _sortingOrderCounter;

            // sortingOrder 카운터 갱신 (한 번만 증가)
            _sortingOrderCounter++;

            return ui;
        }
        else
        {
            T ui = OpenUI<T>();
            return ui;
        }
    }

    // UI가 존재하는지 확인하는 메서드
    public bool IsExistUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;
        return _uiList.ContainsKey(uiName) && _uiList[uiName] != null;
    }

    // 활성화된 UI가 있는지 확인하는 메서드
    public bool ActiveUI()
    {
        foreach (var ui in _uiList.Values)
        {
            if (ui.gameObject.activeSelf)
            {
                return true; // 하나라도 활성화된 UI가 있으면 true 반환
            }
        }

        return false; // 활성화된 UI가 없으면 false 반환
    }
}
