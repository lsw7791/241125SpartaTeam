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
        if (inItem.enhenceCount > 10)
        {
            Debug.Log("�ִ� ��ȭ ��ġ�Դϴ�.");
            UIManager.Instance.ToggleUI<UpGradeUI>();
            return;
        }

        // ������ ������ �˻�
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        // ��ȭ ������ �˻�
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inEnhence);

        if (inItem.enhenceCount == 10)
        {
            Debug.Log("�ִ� ��ȭ ��ġ�Դϴ�.");
            _productText.text = $"{itemData.name} (+Max)";
            _probabilityText[0].text = $"���� Ȯ��\n0%";
            _probabilityText[1].text = $"���� Ȯ��\n0%";
            _probabilityText[2].text = $"�ı� Ȯ��\n0%";
            _ResultTestText.text = "�ְ� ���� �Դϴ�";
            _costText.text = $"��ȭ ���: 0";
            return;
        }

        // ��ȭ Ȯ�� ǥ��

        _productText.text = $"{itemData.name} (+{inEnhence})";
        _probabilityText[0].text = $"���� Ȯ��\n{upgradeData.success[itemData.tier]}%";
        _probabilityText[1].text = $"���� Ȯ��\n{upgradeData.fail[itemData.tier]}%";
        _probabilityText[2].text = $"�ı� Ȯ��\n{upgradeData.Destruction[itemData.tier]}%";
        _costText.text = $"��ȭ ���: {upgradeData.Cost[itemData.tier]}";
        int a = Random.Range(0, 100); // TODO :: ����ġ�� �ٲٱ� �� �׽�Ʈ�� ���� ��
        if (upgradeData.success[itemData.tier] > a)
        {
            inItem.enhenceCount++;
            _ResultTestText.text = "����";
        }
        else
        {
            _ResultTestText.text = "����";
        }
    }

    public void Initialize(InventoryItem inItem)
    {
        item = inItem;

        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        var upgradeData = GameManager.Instance.DataManager.Upgrade.GetData(inItem.enhenceCount);

        _useItemImage.sprite = inItem.ItemIcon;
        _productText.text = $"{itemData.name} (+{inItem.enhenceCount})";
        _probabilityText[0].text = $"���� Ȯ��\n{upgradeData.success[itemData.tier]}%";
        _probabilityText[1].text = $"���� Ȯ��\n{upgradeData.fail[itemData.tier]}%";
        _probabilityText[2].text = $"�ı� Ȯ��\n{upgradeData.Destruction[itemData.tier]}%";
        _ResultTestText.text = "���";
        _costText.text = $"��ȭ ���: {upgradeData.Cost[itemData.tier]}";
    }
}
