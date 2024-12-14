using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // ����â�� �θ� �г�
    [SerializeField] private GameObject craftingSlotPrefab; // ���� ���� UI ������

    private void Start()
    {
        // DataManager���� ���� �����͸� ������ ����â�� �ʱ�ȭ
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
            Button slotButton = newSlot.GetComponent<Button>();

            // ���� Ŭ�� �� �� UI ����
            CraftingSlot craftingSlot = newSlot.GetComponent<CraftingSlot>();
            if (craftingSlot != null)
            {
                craftingSlot.Init(data); // data�� craftingSlot�� ����
            }

            slotButton.onClick.AddListener(() =>
            {
                // ������ ���� �� ���õ� ������ ID�� ����
                GameManager.Instance.craftingManager.SelectItem(data.id);

                // ���õ� �������� �����ִ� UI�� ������Ʈ
                GameManager.Instance.uIManager.OpenUI<CraftingUI>().Init(data);
            });
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = Instantiate(craftingSlotPrefab, craftingPanel);
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
