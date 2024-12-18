using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemUseUI : MonoBehaviour
{
    //private InventoryItem item;
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
                // TODO :: ���� ���� Ÿ������ ������ �ɾ �����ϱ�
                if (inItem.IsEquipped)
                {
                    GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
                    // �κ��� ������ ǥ�� ����
                    GameManager.Instance.Player.equipment.UnEquip(itemData.itemType);
                    // ����
                }
                else
                {
                    GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
                    // �κ��� ������ ǥ��
                    GameManager.Instance.Player.equipment.EquipNew(inItem);
                    // ����
                }
            });
            //UseButton("��ȭ�ϱ�").onClick.AddListener(() =>
            //{ // ��� Ÿ��
            //    Debug.Log("��ȭ");
            //});
            //UseButton("�����ϱ�").onClick.AddListener(() =>
            //{ // ��� Ÿ��
            //    Debug.Log("����");
            //});
            //UseButton("����ϱ�").onClick.AddListener(() =>
            //{ // ���� Ÿ��
            //    Debug.Log("���");
            //});
            //UseButton("������").onClick.AddListener(() =>
            //{ // ��� ������ Ÿ��
            //    Debug.Log("������");
            //});
        }
    }

    private void UseButtonClear()
    {
        if (_useButton != null)
        {
            foreach (GameObject button in _useButton)
            {
                Destroy(button);
            }
            _useButton.Clear();
        }
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
