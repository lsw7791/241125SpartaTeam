using MainData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private Transform craftingPanel; // ����â�� �θ� �г�
    [SerializeField] private GameObject craftingSlotPrefab; // ���� ���� UI ������
    [SerializeField] private List<CraftingData> craftingDataList; // ������ ������ ���

    private void Start()
    {
        // ������ �����Ͱ� ������ �⺻ ������ �Ҵ�
        if (craftingDataList == null || craftingDataList.Count == 0)
        {
            craftingDataList = new List<CraftingData>
            {
                new CraftingData { id = 1, tier = 1, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_1" },
                new CraftingData { id = 2, tier = 2, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_2" },
                new CraftingData { id = 3, tier = 3, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_3" },
                new CraftingData { id = 4, tier = 4, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_4" },
                new CraftingData { id = 5, tier = 5, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_5" },
                new CraftingData { id = 6, tier = 6, imagePath = "Prefabs/Equips/Weapon/Sword/Sword_6" }
            };
        }

        //PopulateCraftingUI(craftingDataList);  // UI�� �������� ä���
    }

    // UI�� ������ ����� ä��� �޼ҵ�
    //public void PopulateCraftingUI(List<CraftingData> dataList)
    //{
    //    // ���� UI �ʱ�ȭ
    //    foreach (Transform child in craftingPanel)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // ������ ������� UI ����
    //    foreach (var data in dataList)
    //    {
    //        // ���� ����
    //        GameObject slot = Instantiate(craftingSlotPrefab, craftingPanel);

    //        // ���Կ� ������ ������ ����
    //        CraftingSlot slotScript = slot.GetComponent<CraftingSlot>();
    //        if (slotScript != null)
    //        {
    //            slotScript.Setup(data); // ���Կ� ������ ����
    //        }
    //        else
    //        {
    //            Debug.LogError("CraftingSlot ��ũ��Ʈ�� �����տ� �����ϴ�.");
    //        }
    //    }
    //}
}
