using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // ����â�� �θ� �г�

    private void Start()
    {
        List<CraftingData> craftingDataList = GameManager.Instance.dataManager.crafting.GetAllDatas();
        PopulateCraftingUI(craftingDataList); // UI�� �������� ä���
    }

    // UI�� ������ ����� ä��� �޼���
    public void PopulateCraftingUI(List<CraftingData> dataList)
    {
        // ���� UI �ʱ�ȭ
        foreach (Transform child in craftingPanel)
        {
            Destroy(child.gameObject);
        }

        // ������ ������� UI ����
        foreach (var data in dataList)
        {
            GameObject newSlot = SlotObject(data);
            Image itemImage = newSlot.AddComponent<Image>();
            Button slotButton = newSlot.AddComponent<Button>();

            if (slotButton != null)
            {
                itemImage.sprite = Resources.Load<Sprite>(data.imagePath);
            }

            slotButton.onClick.AddListener(() =>
            {
                // ������ ���� �� ���õ� ������ ID�� ����
                GameManager.Instance.craftingManager.SelectItem(data.id);

                // ���õ� �������� �����ִ� UI�� ������Ʈ
                CraftingUI craftingUI = GameManager.Instance.uIManager.OpenUI<CraftingUI>();
                craftingUI.Init(data);
            });
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = new GameObject();
        outSlot.transform.parent = craftingPanel;
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
