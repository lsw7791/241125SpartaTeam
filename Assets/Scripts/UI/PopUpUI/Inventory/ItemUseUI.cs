using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseUI : MonoBehaviour
{
    [SerializeField] private Transform _useMenu;
    [SerializeField] private Image _useItemImage;

    private List<GameObject> _useButton = new List<GameObject>();

    public void Initialize(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        _useItemImage.sprite = inItem.ItemIcon;
        UseButtonClear();

        if (itemData.itemType < ItemType.Mine)
        {
            UseButton("�����ϱ�").onClick.AddListener(() =>
            { // ��� Ÿ��
                if (inItem.IsEquipped)
                {
                    GameManager.Instance.Player.equipment.UnEquip(itemData.itemType);
                    // ����
                }
                else
                {
                    GameManager.Instance.Player.equipment.EquipNew(inItem);
                    // ����
                    inItem.IsEquipped = true;
                }
                GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
            });
            UseButton("��ȭ�ϱ�").onClick.AddListener(() =>
            { // ��� Ÿ��
                UpGradeUI upGradeUI = UIManager.Instance.GetUI<UpGradeUI>();
                UIManager.Instance.ToggleUI<UpGradeUI>();
                upGradeUI.Initialize(inItem);
            });
            //UseButton("�����ϱ�").onClick.AddListener(() =>
            //{ // ��� Ÿ��
            //    Debug.Log("����");
            //});
           
            UseButton("������").onClick.AddListener(() =>
            { // ��� ������ Ÿ��
                // TODO :: ������ ��� �������� ���ϴ� �ڵ� �ʿ�
                if(inItem.IsEquipped)
                { // TODO :: ���� UI�� ǥ���ϱ�
                    Debug.Log("�������� ����Դϴ�.");
                    return;
                }
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);
            });
        }
        else if(itemData.itemType > ItemType.Gold)
        {
            UseButton("����ϱ�").onClick.AddListener(() =>
            {
                // ���� ȿ�� ����
                GameManager.Instance.DataManager.Item.UsePotion(inItem.ItemID);
                GameManager.Instance.Player.ConditionUI.UpdateSliders();
            });
            UseButton("������").onClick.AddListener(() =>
            { // ��� ������ Ÿ��
                // TODO :: ������ ��� �������� ���ϴ� �ڵ� �ʿ�
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);
            });
        }
        else
        {
            UseButton("������").onClick.AddListener(() =>
            { // ��� ������ Ÿ��
                // TODO :: ������ ��� �������� ���ϴ� �ڵ� �ʿ�
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);
            });
        }
    }

    private void UseButtonClear()
    {
        if (_useButton == null)
        {
            return;
        }

        foreach (GameObject button in _useButton)
        {
            Destroy(button);
        }
        _useButton.Clear();
    }

    private Button UseButton(string inButtonName)
    {
        GameObject newUseButton = Instantiate(Resources.Load<GameObject>($"{PathInfo.UIPath}ItemUseSlot"));
        newUseButton.transform.SetParent(_useMenu);
        newUseButton.TryGetComponent<ItemUseButton>(out var outButton);
        outButton.UseType(inButtonName);

        Button newButton = newUseButton.AddComponent<Button>();
        _useButton.Add(newUseButton);

        return newButton;
    }
}
