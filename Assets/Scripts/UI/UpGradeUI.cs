using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeUI : UIBase
{
    [SerializeField] private Image _useItemImage;

    [SerializeField] private Button _upgradeResultButton; // ��ȭ ��ư

    [SerializeField] private TMP_Text _productText;
    [SerializeField] private TMP_Text[] _probabilityText;

    [SerializeField] private TMP_Text _ResultTestText;
    [SerializeField] private TMP_Text _costText;

    InventoryItem item;

    private void Start()
    {
        _upgradeResultButton.onClick.AddListener(() =>
        {
            ShowUpgrade(item, item.enhenceCount);
        });
    }

    public void ShowUpgrade(InventoryItem inItem, int inEnhence)
    {
        if (inEnhence >= 10)
        {
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // ������ ������ �˻�
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierData = itemData.tier - 1;

        // ��ȭ ������ �˻�
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inEnhence);

        int a = Random.Range(0, 100); // TODO :: ����ġ�� �ٲٱ� �� �׽�Ʈ�� ���� ��

        if (upgradeData.success[tierData] > a)
        {
            inItem.enhenceCount++;
            _ResultTestText.text = "����";
        }
        else
        {
            _ResultTestText.text = "����";
        }

        // ��ȭ Ȯ�� ǥ��

        _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
        _probabilityText[0].text = $"���� Ȯ��\n{upgradeData.success[tierData]}%";
        _probabilityText[1].text = $"���� Ȯ��\n{upgradeData.fail[tierData]}%";
        _probabilityText[2].text = $"�ı� Ȯ��\n{upgradeData.Destruction[tierData]}%";
        _costText.text = $"��ȭ ���: {upgradeData.Cost[tierData]}";


        if (inItem.enhenceCount == 10)
        {
            _productText.text = $"{itemData.name} (+Max)";
            _probabilityText[0].text = $"���� Ȯ��\n0%";
            _probabilityText[1].text = $"���� Ȯ��\n0%";
            _probabilityText[2].text = $"�ı� Ȯ��\n0%";
            _ResultTestText.text = "�ְ� ���� �Դϴ�";
            _costText.text = $"��ȭ ���: 0";
            return;
        }
    }

    public void Initialize(InventoryItem inItem)
    {
        item = inItem;

        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierData = itemData.tier - 1;
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        _useItemImage.sprite = inItem.ItemIcon;

        CurrentEnhence(itemData, upgradeData);
    }

    private void CurrentEnhence(ItemData inItemData, UpGradeData inUpGradeData)
    {
        int tierData = inItemData.tier - 1;

        if (item.enhenceCount == 10)
        {
            _productText.text = $"{inItemData.name} (+Max)";
            _probabilityText[0].text = $"���� Ȯ��\n0%";
            _probabilityText[1].text = $"���� Ȯ��\n0%";
            _probabilityText[2].text = $"�ı� Ȯ��\n0%";
            _ResultTestText.text = "�ְ� ���� �Դϴ�";
            _costText.text = $"��ȭ ���: 0";
            return;
        }
        else
        {
            _productText.text = $"{inItemData.name} (+{item.enhenceCount})";
            _probabilityText[0].text = $"���� Ȯ��\n{inUpGradeData.success[tierData]}%";
            _probabilityText[1].text = $"���� Ȯ��\n{inUpGradeData.fail[tierData]}%";
            _probabilityText[2].text = $"�ı� Ȯ��\n{inUpGradeData.Destruction[tierData]}%";
            _ResultTestText.text = "���";
            _costText.text = $"��ȭ ���: {inUpGradeData.Cost[tierData]}";
        }
    }
}
