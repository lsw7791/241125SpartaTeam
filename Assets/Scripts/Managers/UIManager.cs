using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject inventoryUI;
    public GameObject mapUI;
    public GameObject questUI;
    public GameObject optionUI;
    public GameObject statusUI;

    public bool isInventoryOpen = false;  // �κ��丮 ���¸� �����ϴ� ����
    public bool isMapOpen = false;        // �� ���� ����
    public bool isQuestOpen = false;      // ����Ʈ ���� ����
    public bool isOptionOpen = false;     // �ɼ� ���� ����
    public bool isStatusOpen = false;     // �����ͽ� ���� ����

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

    // esc Ű�� ��� UI �ݱ�
    public void CloseAllUIs()
    {
        // ��� UI�� ��Ȱ��ȭ
        inventoryUI.SetActive(false);
        mapUI.SetActive(false);
        questUI.SetActive(false);
        optionUI.SetActive(false);
        statusUI.SetActive(false);

        // UI ���� ���� false�� ����
        isInventoryOpen = false;
        isMapOpen = false;
        isQuestOpen = false;
        isOptionOpen = false;
        isStatusOpen = false;
    }
}
