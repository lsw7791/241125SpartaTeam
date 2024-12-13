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
            // ���� ����
            //GameObject slot = Instantiate(craftingSlotPrefab, craftingPanel);
            GameObject slot = new GameObject();
            slot.name = $"{data.name} Slot";

            slot.transform.parent = craftingPanel;
            Image itemImage =  slot.AddComponent<Image>();
            // ���Կ� ������ ������ ����
            CraftingSlot slotScript = slot.AddComponent<CraftingSlot>();

            if (slotScript != null)
            {
                // �����Ϳ��� ��θ� ������� ��������Ʈ �ε�
                Sprite itemSprite = Resources.Load<Sprite>(data.imagePath);

                if (itemSprite != null)
                {
                    //slotScript.Setup(data, itemSprite); // ���Կ� �����Ϳ� ��������Ʈ ����
                    if (itemImage != null && itemSprite != null)
                    {
                        itemImage.sprite = itemSprite;
                    }

                    //if (itemName != null)
                    //{
                    //    itemName.text = data != null ? data.name : "������ ����";
                    //}
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
}
