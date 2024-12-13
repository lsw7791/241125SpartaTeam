using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Tripolygon.UModeler.UI.ViewModels;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // ����â�� �θ� �г�
    //[SerializeField] private GameObject craftingSlotPrefab; // ���� ���� UI ������

    private void Start()
    { // DataManager���� ���� �����͸� ������ ����â�� �ʱ�ȭ
        List<CraftingData> craftingDataList = GameManager.Instance.dataManager.crafting.GetAllDatas();

        if (craftingDataList == null || craftingDataList.Count == 0)
        {
            Debug.LogError("���� �����Ͱ� �����ϴ�.");
            return;
        }

        PopulateCraftingUI(craftingDataList); // UI�� �������� ä���
    }

    // UI�� ������ ����� ä��� �޼���
    public void PopulateCraftingUI(List<CraftingData> dataList)
    { // ���� UI�� �ʱ�ȭ�� ��, ���� �����͸� ������� ���� ���� UI�� ����
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

            // ���Կ� ������ ������ ����
            CraftingSlot slotScript = newSlot.AddComponent<CraftingSlot>();
            slotScript.Init(data);

            if (slotScript != null)
            {
                // �����Ϳ��� ��θ� ������� ��������Ʈ �ε�
                Sprite itemSprite = Resources.Load<Sprite>(data.imagePath);

                if (itemSprite != null)
                {
                    if (itemImage != null && itemSprite != null)
                    {
                        itemImage.sprite = itemSprite;
                    }
                }
                else
                {
                    Debug.LogWarning($"�̹����� ��� '{data.imagePath}'���� �ε��� �� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogError("CraftingSlot ��ũ��Ʈ�� �����տ� �����ϴ�.");
            }
        }
    }

    private GameObject SlotObject(CraftingData inData)
    {
        GameObject outSlot = new GameObject();
        outSlot.name = $"{inData.name} Slot";

        outSlot.transform.parent = craftingPanel;
        outSlot.AddComponent<Button>();

        return outSlot;
    }
}
