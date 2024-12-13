using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{ // ������ ���� ������ UI�� ����
    [SerializeField] private Image itemImage; // ������ �̹��� UI ���
    [SerializeField] private Text itemName;   // ������ �̸� UI ���
    //[SerializeField] private Button craftButton; // ũ����Ʈ ��ư

    private CraftingData craftingData;

    private void OnEnable()
    {
        itemImage = GetComponent<Image>();
    }


    // ���Կ� ������ �����͸� �����ϴ� �޼���
    //public void Setup(CraftingData data, Sprite itemSprite = null)
    //{ // ������ �����͸� UI ���Կ� ����
    //    craftingData = data;

    //    // ������ �̹����� �̸��� ����
    //    if (itemImage != null && itemSprite != null)
    //    {
    //        itemImage.sprite = itemSprite;
    //    }

    //    if (itemName != null)
    //    {
    //        itemName.text = craftingData != null ? craftingData.name : "������ ����";
    //    }
    //}

    // ũ����Ʈ ��ư Ŭ�� �� ó���ϴ� �޼���
    public void OnCraftButtonClick()
    { // ��ư Ŭ�� ��, GameManager�� CraftingManager�� ���� ������ ������ ó��
        // CraftingManager���� ������ ���� �� ũ����Ʈ �õ�
        if (craftingData != null)
        {
            GameManager.Instance.craftingManager.SelectItem(craftingData.id); // ������ ����
        }
    }
    // ���� ������ ������ ���� �ܰ�
}
