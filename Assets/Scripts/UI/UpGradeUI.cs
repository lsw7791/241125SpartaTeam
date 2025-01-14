using MainData;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _enhenceCountText;

    [SerializeField] private Button _upgradeResultButton; // 강화 버튼

    [SerializeField] private TMP_Text _productText;
    [SerializeField] private TMP_Text[] _probabilityText;

    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _costText;

    private int[] _upgradeIndex;

    private InventoryItem _item;

    private void Start()
    {
        _upgradeResultButton.onClick.AddListener(() =>
        {
            ShowUpgrade(_item);
        });
    }

    private void Init(InventoryItem inItem)
    {
        // 아이템 데이터 검색
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierIndex = itemData.tier - 1;

        // 강화 데이터 검색
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        _upgradeIndex = new int[] {
            upgradeData.success[tierIndex],
            upgradeData.fail[tierIndex],
            upgradeData.Destruction[tierIndex]
        };
    }

    public void ShowUpgrade(InventoryItem inItem)
    {
        if (inItem.enhenceCount >= 10)
        {
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        // 아이템 데이터 검색
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierIndex = itemData.tier - 1;

        int coin = GameManager.Instance.Player.stats.Gold;

        if (upgradeData.Cost[tierIndex] > coin)
        {
            _resultText.text = "소지금이 부족하여 강화를 진행할 수 없습니다...";
            return;
        }
        coin -= upgradeData.Cost[tierIndex];

        Init(inItem);
        int totalRange = _upgradeIndex.Sum();

        // 랜덤 가중치
        int randomValue = Random.Range(0, totalRange);

        if (randomValue < _upgradeIndex[0])
        {
            // TODO :: 강화된 능력치 출력
            // TODO :: 강화수치 이미지 출력
            if (inItem.IsEquipped)
            {
                GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, false);
                inItem.enhenceCount++;
                GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, true);
            }
            else
            {
                inItem.enhenceCount++;
            }

            InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
            inventoryUI.Setup(GameManager.Instance.Player.inventory);

            _resultText.text = "강화에 성공하였습니다!";
            if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(5) &&
               !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[5])
            {
                GameManager.Instance.DataManager.MainQuest.CompleteQuest(5);
            }
        }
        else if (randomValue < _upgradeIndex[0] + _upgradeIndex[1])
        {
            // TODO :: 유지된 능력치 출력
            _resultText.text = "강화에 실패하였습니다.";
        }
        else
        {
            if (inItem.IsEquipped)
            {
                GameManager.Instance.Player.inventory.UnEquip(itemData.itemType);
            }

            // TODO :: 금간 장비 이미지? 깨진 이미지 출력
            _resultText.text = "강화에 실패하였습니다. 장비가 파괴되었습니다.";
            GameManager.Instance.Player.inventory.UnEquip(itemData.itemType);

            InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
            inventoryUI.ClearEquipmentSlot(itemData.itemType);
            GameManager.Instance.Player.inventory.RemoveItem(inItem.ItemID, 1);
            UIManager.Instance.ToggleUI<UpGradeUI>();
        }

        UpdateUI(inItem);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.inventory);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.stats);
        GameManager.Instance.Player.stats.nowMapNumber = GameManager.Instance.SceneNum;
    }

    public void Initialize(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        _item = inItem;
        _useItemImage.sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
        UpdateUI(inItem);
    }

    private void UpdateUI(InventoryItem inItem) // UI 업데이트
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
            _probabilityText[0].text = "성공 확률\n0%";
            _probabilityText[1].text = "유지 확률\n0%";
            _probabilityText[2].text = "파괴 확률\n0%";
            _costText.text = $"강화 비용: {upgradeData.Cost[tierIndex]}\n/\n현재 소지금: {coin}";
        }
        else
        {
            _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
            _probabilityText[0].text = $"성공 확률\n{upgradeData.success[tierIndex]}%";
            _probabilityText[1].text = $"유지 확률\n{upgradeData.fail[tierIndex]}%";
            _probabilityText[2].text = $"파괴 확률\n{upgradeData.Destruction[tierIndex]}%";
            _costText.text = $"강화 비용: {upgradeData.Cost[tierIndex]}\n/\n현재 소지금 : {coin}";
        }
    }
}
