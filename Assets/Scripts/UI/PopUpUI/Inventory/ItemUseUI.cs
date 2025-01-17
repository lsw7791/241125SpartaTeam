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

        bool isSprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);

        _useItemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);

        UseButtonClear();

        // ��ư ��� ���� (������ �������� �ٸ��� ó��)
        if (itemData.itemType < ItemType.Mine)
        {
            UseButton("�����ϱ�").onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayButton1SFX();

                if (inItem.IsEquipped)
                {
                    GameManager.Instance.Player.inventory.UnEquip(itemData.itemType);
                    InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
                    inventoryUI.ClearEquipmentSlot(itemData.itemType);
                }
                else
                {
                    GameManager.Instance.Player.inventory.Equip(inItem);
                    InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
                    inventoryUI.UpdateEquipmentSlot(itemData.itemType, UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath));

                    if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(3) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[3])
                    {
                        // QuestCompletionStatus[1]�� false�� ���� CompleteQuest(1) ȣ��
                        GameManager.Instance.DataManager.MainQuest.CompleteQuest(3);
                    }
                }
                GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
            });

            UseButton("��ȭ�ϱ�").onClick.AddListener(() =>
            {
                UpGradeUI upGradeUI = UIManager.Instance.ToggleUI<UpGradeUI>();
                upGradeUI.Initialize(inItem);
                if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(5) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[5])
                {
                    // QuestCompletionStatus[1]�� false�� ���� CompleteQuest(1) ȣ��
                    GameManager.Instance.DataManager.MainQuest.CompleteQuest(5);
                }
            });

            UseButton("������").onClick.AddListener(() =>
            {
                if (inItem.IsEquipped)
                {
                    Debug.Log("�������� ����Դϴ�.");
                    return;
                }

                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);

                // ������ ���� Ȯ�� �� 0�̸� UI ��Ȱ��ȭ
                if (inItem.Quantity <= 0)
                {
                    // ������ ������ 0�̸� ItemUseUI ��Ȱ��ȭ
                    gameObject.SetActive(false);
                }
            });
        }
        else if (itemData.itemType > ItemType.Gold)
        {
            UseButton("����ϱ�").onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayButton1SFX();
                GameManager.Instance.Player.inventory.RemoveItem(inItem.ItemID, 1);
                GameManager.Instance.DataManager.Item.UsePotion(inItem.ItemID);
                GameManager.Instance.Player.ConditionUI.UpdateSliders();

                // ������ ��� �� ������ 0�̸� UI ��Ȱ��ȭ
                if (inItem.Quantity <= 0)
                {
                    gameObject.SetActive(false);
                }
            });

            UseButton("������").onClick.AddListener(() =>
            {
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);

                // ������ ���� Ȯ�� �� 0�̸� UI ��Ȱ��ȭ
                if (inItem.Quantity <= 0)
                {
                    gameObject.SetActive(false);
                }
            });
        }
        else
        {
            UseButton("������").onClick.AddListener(() =>
            {
                GameManager.Instance.Player.inventory.DropItem(inItem.ItemID);

                // ������ ���� Ȯ�� �� 0�̸� UI ��Ȱ��ȭ
                if (inItem.Quantity <= 0)
                {
                    gameObject.SetActive(false);
                }
            });
        }

        UseButton("������ ����").onClick.AddListener(() =>
        {
            ItemDescriptionUI itemDescriptionUI = UIManager.Instance.ToggleUI<ItemDescriptionUI>();
            itemDescriptionUI.Initialize(inItem);
        });
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
