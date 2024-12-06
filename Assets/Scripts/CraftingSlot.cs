using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; // ������ �̹��� UI ���
    [SerializeField] private Text itemName;   // ������ �̸� UI ���
    [SerializeField] private Button craftButton; // ũ����Ʈ ��ư

    private CraftingData craftingData;


    // ���Կ� ������ �����͸� �����ϴ� �޼���
    public void Setup(CraftingData data, Sprite itemSprite = null)
    {
        craftingData = data;

        // ������ �̹����� �̸��� ����
        if (itemImage != null && itemSprite != null)
        {
            itemImage.sprite = itemSprite;
        }

        if (itemName != null)
        {
            itemName.text = craftingData != null ? craftingData.id.ToString() : "������ ����";
        }
    }

    // ũ����Ʈ ��ư Ŭ�� �� ó���ϴ� �޼���
    public void OnCraftButtonClick()
    {
        // CraftingManager���� ������ ���� �� ũ����Ʈ �õ�
        if (craftingData != null)
        {
            GameManager.Instance.craftingManager.SelectItem(craftingData.id); // ������ ����
        }
    }
}
