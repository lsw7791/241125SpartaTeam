using MainData;
using UnityEngine;
using System.Collections.Generic;

public class CraftUI : UIBase
{
    [SerializeField] private Transform craftingPanel; // ����â�� �θ� �г�
    [SerializeField] private GameObject craftingSlotPrefab; // ���� ���� UI ������

    private void Start()
    {
        // DataManager���� ���� �����͸� ��������
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
    {
        // ���� UI �ʱ�ȭ
        foreach (Transform child in craftingPanel)
        {
            Destroy(child.gameObject);
        }

        // ������ ������� UI ����
        foreach (var data in dataList)
        {
            // ���� ����
            GameObject slot = Instantiate(craftingSlotPrefab, craftingPanel);

            // ���Կ� ������ ������ ����
            CraftingSlot slotScript = slot.GetComponent<CraftingSlot>();
            if (slotScript != null)
            {
                // �����Ϳ��� ��θ� ������� ��������Ʈ �ε�
                Sprite itemSprite = Resources.Load<Sprite>(data.imagePath);

                if (itemSprite != null)
                {
                    slotScript.Setup(data, itemSprite); // ���Կ� �����Ϳ� ��������Ʈ ����
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
