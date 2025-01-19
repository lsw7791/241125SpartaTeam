using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _enhenceCountText;

    [SerializeField] private Button _upgradeResultButton; // ��ȭ ��ư

    [SerializeField] private TMP_Text _productText;
    [SerializeField] private TMP_Text[] _probabilityText;

    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _costText;

    private int[] _upgradeIndex;

    private InventoryItem _item;

    int totalRange = 0;

    private void Start()
    {
        _upgradeResultButton.onClick.AddListener(() =>
        {
            ShowUpgrade();
        });
    }

    private int RangeIndex()
    {
        // ������ ������ �˻�
        var itemData = GameManager.Instance.DataManager.GetItemDataById(_item.ItemID);
        int tierIndex = itemData.tier - 1;

        // ��ȭ ������ �˻�
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(_item.enhenceCount);

        _upgradeIndex = new int[] {
            upgradeData.success[tierIndex],
            upgradeData.fail[tierIndex],
            upgradeData.Destruction[tierIndex]
        };

        return _upgradeIndex.Sum();
    }

    public void ShowUpgrade()
    {
        if (_item.enhenceCount >= 10)
        {
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(_item.enhenceCount);

        // ������ ������ �˻�
        var itemData = GameManager.Instance.DataManager.GetItemDataById(_item.ItemID);
        int tierIndex = itemData.tier - 1;

        if (upgradeData.Cost[tierIndex] > GameManager.Instance.Player.stats.Gold)
        {
            _resultText.text = "�������� �����Ͽ� ��ȭ�� ������ �� �����ϴ�...";
            return;
        }
        GameManager.Instance.Player.stats.Gold -= upgradeData.Cost[tierIndex];

        // ���� ����ġ
        int randomValue = Random.Range(0, totalRange);

        if (randomValue < _upgradeIndex[0])
        {
            // TODO :: ��ȭ�� �ɷ�ġ ���
            // TODO :: ��ȭ��ġ �̹��� ���
            if (_item.IsEquipped)
            {
                GameManager.Instance.Player.stats.PlayerStatsUpdate(_item, false);
                _item.enhenceCount++;
                GameManager.Instance.Player.stats.PlayerStatsUpdate(_item, true);
            }
            else
            {
                _item.enhenceCount++;
            }
            totalRange = RangeIndex();

            InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
            inventoryUI.Setup(GameManager.Instance.Player.inventory);

            _resultText.text = "��ȭ�� �����Ͽ����ϴ�!";
            if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(5) &&
               !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[5])
            {
                GameManager.Instance.DataManager.MainQuest.CompleteQuest(5);
            }
        }
        else if (randomValue < _upgradeIndex[0] + _upgradeIndex[1])
        {
            // TODO :: ������ �ɷ�ġ ���
            _resultText.text = "��ȭ�� �����Ͽ����ϴ�.";
        }
        else
        {
            if (_item.IsEquipped)
            {
                GameManager.Instance.Player.inventory.UnEquip(itemData.itemType);
            }

            // TODO :: �ݰ� ��� �̹���? ���� �̹��� ���
            _resultText.text = "��ȭ�� �����Ͽ����ϴ�. ��� �ı��Ǿ����ϴ�.";
            GameManager.Instance.Player.inventory.UnEquip(itemData.itemType);

            InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
            inventoryUI.ClearEquipmentSlot(itemData.itemType);
            GameManager.Instance.Player.inventory.RemoveItem(_item.ItemID, 1);
            UIManager.Instance.ToggleUI<UpGradeUI>();
        }

        UpdateUI(_item);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.inventory);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.stats);
        GameManager.Instance.Player.stats.nowMapNumber = GameManager.Instance.SceneNum;
    }

    public void Initialize(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        _item = inItem;
        _useItemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        UpdateUI(inItem);
        totalRange = RangeIndex();
    }

    private void UpdateUI(InventoryItem inItem) // UI ������Ʈ
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);
        int tierIndex = itemData.tier - 1;
        _enhenceCountText.text = $"{inItem.enhenceCount}";
        _backgroundImage.color = inItem.TierColoer(tierIndex);

        int coin = GameManager.Instance.Player.stats.Gold;

        if (inItem.enhenceCount >= 10)
        {
            _productText.text = $"{itemData.name} (+Max)";
            _probabilityText[0].text = "���� Ȯ��\n0%";
            _probabilityText[1].text = "���� Ȯ��\n0%";
            _probabilityText[2].text = "�ı� Ȯ��\n0%";
            _costText.text = $"��ȭ ���: 0\n/\n���� ������: {coin}";
        }
        else
        {
            _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
            _probabilityText[0].text = $"���� Ȯ��\n{upgradeData.success[tierIndex]}%";
            _probabilityText[1].text = $"���� Ȯ��\n{upgradeData.fail[tierIndex]}%";
            _probabilityText[2].text = $"�ı� Ȯ��\n{upgradeData.Destruction[tierIndex]}%";
            _costText.text = $"��ȭ ���: {upgradeData.Cost[tierIndex]}\n/\n���� ������ : {coin}";
        }
    }
}
