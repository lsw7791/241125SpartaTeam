using MainData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopSlot : MonoBehaviour
{
    // UI ��ҵ�
    public Image icon; // ������ ������
    public TMP_Text name; // ������ �̸� 
    public TMP_Text desc; // ������ ���� 
    public TMP_Text gold; // ������ ���� 
    // ������ ������ ���� �޼���

    public void Setup(ItemData itemData)
    {
        // ������ �����Ϳ� �°� UI ��ҵ��� ����
        icon.sprite = Resources.Load<Sprite>(itemData.spritePath); // ������ ��ηκ��� �̹��� �ε�
        name.text = itemData.name; // ������ �̸�
        desc.text = itemData.desc; // ������ ����
        gold.text = $"{itemData.buy} ���"; // ������ ����
    }

    public void OnclickedSlot()
    {
        GameManager.Instance.uIManager.ToggleUI<BuyUI>();
    }
}
