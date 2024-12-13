using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    // UI ��ҵ�
    public TMP_Text itemNameText;   // ������ �̸� ǥ��
    public TMP_Text itemPriceText;  // ������ ���� ǥ��
    public Image icon;              // ������ ������
    public TMP_Text itemDescText;   // ������ ���� ǥ��
    public Button buyButton;        // ������ ���� ��ư

    private ItemData currentItemData; // ���� ���Կ� �Ҵ�� ������ ������

    public delegate void ItemClickHandler(ItemData itemData);
    public event ItemClickHandler OnItemClick; // ������ Ŭ�� �� �߻��ϴ� �̺�Ʈ

   
    // ������ �����͸� ���Կ� ����
    public void Setup(ItemData itemData)
    {
        currentItemData = itemData;

        // UI�� ������ �̸�, ����, ����, ������ ����
        itemNameText.text = itemData.name;
        itemPriceText.text = $"{itemData.buy} Gold";
        itemDescText.text = itemData.desc;
        icon.sprite = Resources.Load<Sprite>(itemData.spritePath); // ������ ��ηκ��� �̹��� �ε�

        // ���� ��ư Ȱ��ȭ ���� ����
        buyButton.interactable = GameManager.Instance.player.stats.Gold >= itemData.buy;

        // ������ Ŭ�� �� �߻��ϴ� �̺�Ʈ ����
        buyButton.onClick.AddListener(() => OnItemClick?.Invoke(itemData));
    }
  

    // ���� Ŭ�� �� ȣ�� (������ Ŭ�� �� BuyUI Ȱ��ȭ)
    public void OnclickedSlot()
    {
        // BuyUI Ȱ��ȭ �� ������ ������ ����
        if (currentItemData != null)
        {
            GameManager.Instance.uIManager.ToggleUI<BuyUI>();

            // BuyUI�� ������ ������ ����
            BuyUI buyUI = GameManager.Instance.uIManager.GetUI<BuyUI>();
            if (buyUI != null)
            {
                buyUI.SetUp(currentItemData); // BuyUI�� ������ ������ ����
            }
        }
    }
}
