using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
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
    public void Setup(ItemData itemData, ShopType shopType)
    {
        currentItemData = itemData;

        // UI�� ������ �̸�, ����, ����, ������ ����
        itemNameText.text = itemData.name;
        itemPriceText.text = $"{itemData.buy} Gold";
        itemDescText.text = itemData.desc;

        // ShopType�� ���� ������ ��� ����
        if (shopType == ShopType.PotionShop)
        {
            icon.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        }
        else if(shopType == ShopType.WeaponShop || shopType == ShopType.ArmorShop)
        {
            icon.sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
        }
        else if(shopType == ShopType.TarvenShop)
        {
            icon.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        }

        // ���� ��ư Ȱ��ȭ ���� ����
        buyButton.interactable = true;

        // ������ Ŭ�� �� �߻��ϴ� �̺�Ʈ ����
        buyButton.onClick.AddListener(() => OnItemClick?.Invoke(itemData));
    }



    // ���� Ŭ�� �� ȣ�� (������ Ŭ�� �� BuyUI Ȱ��ȭ)
    public void OnclickedSlot()
    {
        // BuyUI Ȱ��ȭ �� ������ ������ ����
        UIManager.Instance.ToggleUI<BuyUI>();
        BuyUI buyUI = UIManager.Instance.OpenUI<BuyUI>();
        buyUI.transform.SetAsLastSibling();

        // BuyUI�� ������ ������ ����
        buyUI.SetUp(currentItemData); // BuyUI�� ������ ������ ����
    }
}
