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
        // TODO :: ��ɺ��� �������ߵ�
        if (itemData.itemType < ItemType.Mine)
        {
            UseButton("�����ϱ�").onClick.AddListener(() =>
            { // ��� Ÿ��
                if (inItem.IsEquipped)
                {
                    GameManager.Instance.Player.inventory.UnEquip(inItem);

                    // ����
                }
                else
                {
                    GameManager.Instance.Player.inventory.EquipNew(inItem);
                    if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(4) &&
                                   !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[4])
                    {
                        GameManager.Instance.DataManager.MainQuest.CompleteQuest(4);
                    }
                    // ����
                }
                GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
            });
            UseButton("��ȭ�ϱ�").onClick.AddListener(() =>
            { // ��� Ÿ��
                UpGradeUI upGradeUI = UIManager.Instance.ToggleUI<UpGradeUI>();
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
                GameManager.Instance.Player.inventory. RemoveItem(inItem.ItemID, 1);
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
