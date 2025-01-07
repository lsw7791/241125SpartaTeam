using Constants;
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

        bool isSprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);

        if (isSprite)
        {
            _useItemImage.sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
        }
        else
        {
            _useItemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        }
        UseButtonClear();
        // TODO :: 기능별로 나눠놔야됨
        if (itemData.itemType < ItemType.Mine)
        {
            UseButton("착용하기").onClick.AddListener(() =>
            { // 장비 타입
                if (inItem.IsEquipped)
                {
                    GameManager.Instance.Player.inventory.UnEquip(inItem);

                    // 해제
                }
                else
                {
                    GameManager.Instance.Player.inventory.EquipNew(inItem);
                    if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(4) &&
                                   !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[4])
                    {
                        GameManager.Instance.DataManager.MainQuest.CompleteQuest(4);
                    }
                    // 착용
                }
                GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
            });
            UseButton("강화하기").onClick.AddListener(() =>
            { // 장비 타입
                UpGradeUI upGradeUI = UIManager.Instance.ToggleUI<UpGradeUI>();
                upGradeUI.Initialize(inItem);
            });
            //UseButton("조합하기").onClick.AddListener(() =>
            //{ // 재료 타입
            //    Debug.Log("조합");
            //});
           
            UseButton("버리기").onClick.AddListener(() =>
            { // 모든 아이템 타입
                // TODO :: 버릴때 몇개를 버릴건지 정하는 코드 필요
                if(inItem.IsEquipped)
                { // TODO :: 동적 UI로 표시하기
                    Debug.Log("착용중인 장비입니다.");
                    return;
                }
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);
            });
        }
        else if(itemData.itemType > ItemType.Gold)
        {
            UseButton("사용하기").onClick.AddListener(() =>
            {
                GameManager.Instance.Player.inventory. RemoveItem(inItem.ItemID, 1);
                // 포션 효과 적용
                GameManager.Instance.DataManager.Item.UsePotion(inItem.ItemID);
                GameManager.Instance.Player.ConditionUI.UpdateSliders();
            });
            UseButton("버리기").onClick.AddListener(() =>
            { // 모든 아이템 타입
                // TODO :: 버릴때 몇개를 버릴건지 정하는 코드 필요
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
