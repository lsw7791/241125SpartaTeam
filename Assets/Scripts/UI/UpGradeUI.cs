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

    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _costText;

    private InventoryItem _item;

    private void Start()
    {
        _upgradeResultButton.onClick.AddListener(() =>
        {
            ShowUpgrade(_item);
        });
    }

    public void ShowUpgrade(InventoryItem inItem)
    {
        if (inItem.enhenceCount >= 10)
        {
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // ������ ������ �˻�
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierIndex = itemData.tier - 1;

        // ��ȭ ������ �˻�
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        // ���� ����ġ
        int randomValue = Random.Range(0, 100);

        if (randomValue < upgradeData.success[tierIndex])
        {
            inItem.enhenceCount++;
            // TODO :: ��ȭ�� �ɷ�ġ ���
            // TODO :: ��ȭ��ġ �̹��� ���
            _resultText.text = "����";
        }
        else if (randomValue < upgradeData.success[tierIndex] + upgradeData.fail[tierIndex])
        {
            // TODO :: ������ �ɷ�ġ ���
            _resultText.text = "����";
        }
        else
        {
            // TODO :: �ݰ� ��� �̹���? ���� �̹��� ���
            _resultText.text = "�ı�";
        }

        UpdateUI(inItem);
    }

    public void Initialize(InventoryItem inItem)
    {
        _item = inItem;
        _useItemImage.sprite = inItem.ItemIcon;
        UpdateUI(inItem);
    }

    private void UpdateUI(InventoryItem inItem) // UI ������Ʈ
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);
        int tierIndex = itemData.tier - 1;

        if (inItem.enhenceCount >= 10)
        {
            _productText.text = $"{itemData.name} (+Max)";
            _probabilityText[0].text = "���� Ȯ��\n0%";
            _probabilityText[1].text = "���� Ȯ��\n0%";
            _probabilityText[2].text = "�ı� Ȯ��\n0%";
            _costText.text = $"��ȭ ���: 0\n/\n���� ������";
        }
        else
        {
            _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
            _probabilityText[0].text = $"���� Ȯ��\n{upgradeData.success[tierIndex]}%";
            _probabilityText[1].text = $"���� Ȯ��\n{upgradeData.fail[tierIndex]}%";
            _probabilityText[2].text = $"�ı� Ȯ��\n{upgradeData.Destruction[tierIndex]}%";
            _costText.text = $"��ȭ ���: {upgradeData.Cost[tierIndex]}\n/\n���� ������";
        }
    }
}
