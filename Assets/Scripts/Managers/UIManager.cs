using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject inventoryUI;
    public GameObject mapUI;
    public GameObject questUI;
    public GameObject optionUI;
    public GameObject statusUI;


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
}
