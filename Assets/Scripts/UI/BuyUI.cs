using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUI : UIBase
{
    [SerializeField] TMP_Text _text;        // ���� Ȯ�� �ؽ�Ʈ
    [SerializeField] GameObject _buyBtnObj; // ���� ��ư ������Ʈ
    [SerializeField] Button _buyBtn;       // ���� ��ư
    [SerializeField] ItemData _itemData;

    public void SetUp(ItemData itemData)
    {
        _itemData = itemData; // ������ ������ ����

        // ���� ������ ���� ��
        bool canBuy = GameManager.Instance.player.stats.Gold >= itemData.buy;

        if (canBuy)
        {
            _text.text = "�����Ͻðڽ��ϱ�?";
            ToggleBuyButton(true); // ��ư Ȱ��ȭ
            _buyBtn.onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
            _buyBtn.onClick.AddListener(() => PurchaseItem(itemData)); // ���� �̺�Ʈ �߰�
        }
        else
        {
            ToggleBuyButton(false); // ��ư ��Ȱ��ȭ
        }
    }

    private void ToggleBuyButton(bool isActive)
    {
        _buyBtnObj.SetActive(isActive);
        _buyBtn.interactable = isActive;
    }

    public void PurchaseItem(ItemData itemData)
    {
        // ��� ����
        GameManager.Instance.player.stats.Gold -= itemData.buy;

        // �Ϸ� �޽���
        _text.text = $"{itemData.name}��(��) �����Ͽ����ϴ�!";

        // ��������Ʈ ��ο��� Sprite ��ü�� �ε�
        Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);

        // �������� �κ��丮�� �߰�
        GameManager.Instance.player.inventory.AddItem(
            itemData.id.ToString(),  // ������ ID�� ���ڿ��� ��ȯ
            itemData.name,           // ������ �̸�
            1,                       // ������ ���� (���� �� 1��)
            itemData.itemType,           // ������ Ÿ��
            itemSprite               // ������ ��������Ʈ
        );

        ToggleBuyButton(false); // ��ư ��Ȱ��ȭ
    }

}
