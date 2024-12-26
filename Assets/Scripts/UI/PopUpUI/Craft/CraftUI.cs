using MainData;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftUI : UIBase
{
    [SerializeField] private Transform _craftingPanel; // ����â�� �θ� �г�

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
            Image itemImage = newSlot.AddComponent<Image>();
            Button slotButton = newSlot.AddComponent<Button>();

            if (slotButton != null)
            {
                itemImage.sprite = Resources.Load<Sprite>(data.imagePath);

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
        GameObject outSlot = new GameObject();
        outSlot.transform.parent = _craftingPanel;
        outSlot.name = $"{inData.name} Slot";
        return outSlot;
    }
}
