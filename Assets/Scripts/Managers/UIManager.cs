using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject inventoryUI;
    public GameObject mapUI;
    public GameObject questUI;
    public GameObject optionUI;
    public GameObject statusUI;

    public bool isInventoryOpen = false;  // 인벤토리 상태를 추적하는 변수
    public bool isMapOpen = false;        // 맵 상태 추적
    public bool isQuestOpen = false;      // 퀘스트 상태 추적
    public bool isOptionOpen = false;     // 옵션 상태 추적
    public bool isStatusOpen = false;     // 스태터스 상태 추적

    private GameObject canvasInstance;

    protected override void Awake()
    {
        base.Awake();

        GameObject canvasPrefab = Resources.Load<GameObject>("Prefabs/UI/Canvas");
        canvasInstance = Instantiate(canvasPrefab);
        canvasInstance.transform.SetParent(this.transform);

        Transform popupUI = canvasInstance.transform.Find("PopupUI");

        inventoryUI = popupUI.Find("InventoryUI")?.gameObject;
        mapUI = popupUI.Find("MapUI")?.gameObject;
        questUI = popupUI.Find("QuestUI")?.gameObject;
        optionUI = popupUI.Find("OptionUI")?.gameObject;
        statusUI = popupUI.Find("StatusUI")?.gameObject;
    }

    // esc 키로 모든 UI 닫기
    public void CloseAllUIs()
    {
        // 모든 UI를 비활성화
        inventoryUI.SetActive(false);
        mapUI.SetActive(false);
        questUI.SetActive(false);
        optionUI.SetActive(false);
        statusUI.SetActive(false);

        // UI 상태 값을 false로 설정
        isInventoryOpen = false;
        isMapOpen = false;
        isQuestOpen = false;
        isOptionOpen = false;
        isStatusOpen = false;
    }
}
