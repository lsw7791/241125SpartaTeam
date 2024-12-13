using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : UIBase
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

        // ���� ������ ���� ��
        bool canBuy = GameManager.Instance.player.stats.Gold >= itemData.buy;

        if (canBuy)
        {
            _text.text = "������ �Է����ּ���.";
            ToggleBuyButton(true); // ��ư Ȱ��ȭ
            ToggleInputFieldParent(true); // ���� �Է� �ʵ� �θ� Ȱ��ȭ

            _buyBtn.onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
            _buyBtn.onClick.AddListener(() => PurchaseItem(itemData)); // ���� �̺�Ʈ �߰�
        }
        else
        {
            ToggleBuyButton(false); // ��ư ��Ȱ��ȭ
            ToggleInputFieldParent(false); // ���� �Է� �ʵ� �θ� ��Ȱ��ȭ
        }
    }

    private void ToggleBuyButton(bool isActive)
    {
        _buyBtnObj.SetActive(isActive);
        _buyBtn.interactable = isActive;
    }

    private void ToggleInputFieldParent(bool isActive)
    {
        _inputFieldParent.SetActive(isActive); // ���� �Է� �ʵ� �θ� ��ü Ȱ��ȭ/��Ȱ��ȭ
    }

    public void PurchaseItem(ItemData itemData)
    {
        // ���� �Է°��� ��������
        int quantity;
        if (!int.TryParse(_quantityInputField.text, out quantity))
        {
            // ���ڰ� �ƴ� �Է��� ���
            _text.text = "���ڷ� �Է����ּ���.";
            return;
        }

        quantity = Mathf.Max(quantity, 1); // ������ 1 �̻��� �ǵ���

        // �� �ݾ� ���
        int totalCost = itemData.buy * quantity;

        if (GameManager.Instance.player.stats.Gold >= totalCost)
        {
            // ��� ����
            GameManager.Instance.player.stats.Gold -= totalCost;

            // �Ϸ� �޽���
            _text.text = $"���� �Ϸ�!";

            // ��������Ʈ ��ο��� Sprite ��ü�� �ε�
            Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);

            // �������� �κ��丮�� �߰�
            GameManager.Instance.player.inventory.AddItem(
                itemData.id.ToString(),  // ������ ID�� ���ڿ��� ��ȯ
                itemData.name,           // ������ �̸�
                quantity,                // ������ ����
                itemData.itemType,       // ������ Ÿ��
                itemSprite               // ������ ��������Ʈ
            );

            ToggleBuyButton(false); // ��ư ��Ȱ��ȭ
            ToggleInputFieldParent(false); // ���� �Է� �ʵ� �θ� ��Ȱ��ȭ
        }
        else
        {
            // ��尡 �����ϸ� ��� �޽���
            _text.text = "��尡 �����մϴ�.";
        }
    }
}
