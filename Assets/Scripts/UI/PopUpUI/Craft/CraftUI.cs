using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CraftUI : UIBase
{
    [SerializeField] private Transform _craftingPanel; // ����â�� �θ� �г�
    public ScrollRect scrollRect;

    [Header("DescriptionUI")]
    public GameObject itemObject;
    public Image itemImage; // ���Կ� ǥ�õ� ������ �̹���
    public TMP_Text itemName;  // ������ �̸� �ؽ�Ʈ
    public TMP_Text itemDescription;

    private void Start()
    {
        List<CraftingData> craftingDataList = GameManager.Instance.DataManager.Crafting.GetAllDatas();
        PopulateCraftingUI(craftingDataList); // UI�� �������� ä���
    }

    // UI�� ������ ����� ä��� �޼���
    private void PopulateCraftingUI(List<CraftingData> inDataList)
    {
        // ���� UI �ʱ�ȭ
        foreach (Transform child in _craftingPanel)
        {
            Destroy(child.gameObject);
        }

        // ������ ������� UI ����
        foreach (var data in inDataList)
        {
            GameObject newSlot = SlotObject(data);
            //CraftSlot craftSlot = newSlot.AddComponent<CraftSlot>();
            //Image itemImage = newSlot.AddComponent<Image>();
            Button slotButton = newSlot.AddComponent<Button>();

            if (slotButton != null)
            {
                newSlot.TryGetComponent<CraftSlot>(out var outCraftSlot);

                outCraftSlot.Initialize(data);
                //itemImage.sprite = Resources.Load<Sprite>(data.imagePath);
                //itemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(data.atlasPath);
                slotButton.onClick.AddListener(() =>
                {
                    // ������ ���� �� ���õ� ������ ID�� ����
                    GameManager.Instance.CraftingManager.SelectItem(data.id);

                    // ���õ� �������� �����ִ� UI�� ��������, ���� ������ ����
                    UIManager.Instance.ToggleUI<CraftingUI>();
                    CraftingUI craftingUI = UIManager.Instance.OpenUI<CraftingUI>();

                    // ���õ� ������ �����͸� UI�� �ʱ�ȭ
                    craftingUI.Init(data);
                });
            }
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        //GameObject outSlot = new GameObject();
        var slotPrefab = Resources.Load<GameObject>("Prefabs/Items/CraftSlot");

        GameObject outSlot = Instantiate(slotPrefab, _craftingPanel);
        //outSlot.transform.parent = _craftingPanel;
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
