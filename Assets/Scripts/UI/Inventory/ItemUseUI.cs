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
            UseButton("착용하기").onClick.AddListener(() =>
            { // 장비 타입
                if (inItem.IsEquipped)
                {
                    GameManager.Instance.Player.equipment.UnEquip(itemData.itemType);
                    // 해제
                }
                else
                {
                    GameManager.Instance.Player.equipment.EquipNew(inItem);
                    // 착용
                    inItem.IsEquipped = true;
                }
                GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
            });
            UseButton("강화하기").onClick.AddListener(() =>
            { // 장비 타입
                UpGradeUI upGradeUI = UIManager.Instance.GetUI<UpGradeUI>();
                UIManager.Instance.ToggleUI<UpGradeUI>();
                upGradeUI.Initialize(inItem);
            });
            //UseButton("조합하기").onClick.AddListener(() =>
            //{ // 재료 타입
            //    Debug.Log("조합");
            //});
            //UseButton("사용하기").onClick.AddListener(() =>
            //{ // 포션 타입
            //    Debug.Log("사용");
            //});
            UseButton("버리기").onClick.AddListener(() =>
            { // 모든 아이템 타입
                // TODO :: 버릴때 몇개를 버릴건지 정하는 코드 필요
                if(inItem.IsEquipped)
                {
                    Debug.Log("착용중인 장비입니다.");
                    return;
                }
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);
            });
        }
        else
        {
            UseButton("버리기").onClick.AddListener(() =>
            { // 모든 아이템 타입
                // TODO :: 버릴때 몇개를 버릴건지 정하는 코드 필요
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);
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
