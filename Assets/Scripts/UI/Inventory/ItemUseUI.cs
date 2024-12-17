using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseUI : MonoBehaviour
{
    //private InventoryItem item;
    [SerializeField] private Transform _useMenu;
    [SerializeField] private Image _useItemImage;

    private List<GameObject> _useButton = new List<GameObject>();

    private void Start()
    {
        //UseButtonClear();

        //if (item.itemUseType == ItemUseType.Equipment)
        //{
        //    UseButton("����").onClick.AddListener(() =>
        //    {
        //        Debug.Log("����");
        //    });
        //    UseButton("��ȭ").onClick.AddListener(() =>
        //    {
        //        Debug.Log("��ȭ");
        //    });
        //    UseButton("������").onClick.AddListener(() =>
        //    {
        //        Debug.Log("������");
        //    });
        //}
    }

    public void Initialize(InventoryItem inItem)
    {
        _useItemImage.sprite = inItem.ItemIcon;

        UseButtonClear();

        if (inItem.itemUseType == ItemUseType.Equipment)
        {
            UseButton("����ϱ�").onClick.AddListener(() =>
            {
                GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
                Debug.Log("����");
            });
            UseButton("��ȭ�ϱ�").onClick.AddListener(() =>
            {
                Debug.Log("��ȭ");
            });
            UseButton("������").onClick.AddListener(() =>
            {
                Debug.Log("������");
            });
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
