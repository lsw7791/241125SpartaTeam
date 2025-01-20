using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellUI : UIBase
{
    [SerializeField] TMP_Text _text;        // ���� Ȯ�� �ؽ�Ʈ
    [SerializeField] GameObject _buyBtnObj; // ���� ��ư ������Ʈ
    [SerializeField] Button _buyBtn;        // ���� ��ư
    [SerializeField] ItemData _itemData;
    [SerializeField] TMP_InputField _quantityInputField; // ���� �Է� �ʵ�
    [SerializeField] GameObject _inputFieldParent;

    public void SetUp(ItemData itemData)
    {
        _itemData = itemData; // ������ ������ ����

        _text.text = "������ �Է����ּ���.";
        ToggleBuyButton(true); // ��ư Ȱ��ȭ          
        ToggleInputFieldParent(true); // ���� �Է� �ʵ� �θ� Ȱ��ȭ

        _buyBtn.onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
        _buyBtn.onClick.AddListener(() => PurchaseItem(itemData)); // ���� �̺�Ʈ �߰�

    }

    private void ToggleBuyButton(bool isActive)
    {
        SoundManager.Instance.PlayButton1SFX();
        _buyBtnObj.SetActive(isActive);
        _buyBtn.interactable = isActive;
    }

    private void ToggleInputFieldParent(bool isActive)
    {
        _inputFieldParent.SetActive(isActive); // ���� �Է� �ʵ� �θ� ��ü Ȱ��ȭ/��Ȱ��ȭ
    }
    private void UpdateGoldDisplay()
    {
        // ���� ��� �ؽ�Ʈ ������Ʈ
        //_hasGold.text = $"�������: {GameManager.Instance.player.stats.Gold}";
    }
    public void PurchaseItem(ItemData itemData)
    {
        bool isEquip = GameManager.Instance.Player.inventory.IsItemEquipped(itemData);
        if (isEquip)
        {
            _text.text = "�������� ����Դϴ�.";
            return;
        }
        // ���� �Է°��� ��������
        int quantity;

        if (!int.TryParse(_quantityInputField.text, out quantity))
        {
            // ���ڰ� �ƴ� �Է��� ���
            _text.text = "���ڷ� �Է����ּ���.";
            return;
        }

        if (GameManager.Instance.Player.inventory.GetItemCount(itemData.id) < quantity)
        {
            quantity = GameManager.Instance.Player.inventory.GetItemCount(itemData.id);
        }

        quantity = Mathf.Max(quantity, 1); // ������ 1 �̻��� �ǵ���

        // �� �ݾ� ���
        int totalCost = itemData.sell * quantity;
        // ��� �Ա�
        GameManager.Instance.Player.stats.Gold += totalCost;

        ShopUI shopUI;
        shopUI = UIManager.Instance.GetUI<ShopUI>();
        shopUI.HasGold.text = GameManager.Instance.Player.stats.Gold.ToString();

        // �������� �κ��丮���� ����
        GameManager.Instance.Player.inventory.RemoveItem(itemData.id, quantity); // ���� ����
        shopUI.SetupShopUI();

        ToggleBuyButton(false); // ��ư ��Ȱ��ȭ
        ToggleInputFieldParent(false); // ���� �Է� �ʵ� �θ� ��Ȱ��ȭ
        _text.text = "�ǸŰ� �Ϸ�Ǿ����ϴ�.";
        if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(6) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[6])
        {
            GameManager.Instance.DataManager.MainQuest.CompleteQuest(6);
        }
    }
}

