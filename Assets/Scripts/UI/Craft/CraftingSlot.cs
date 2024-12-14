using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{ // ������ ���� ������ UI�� ����
    private Button craftButton; // ���� ��ư
    private CraftingData craftingData;

    private void Start()
    {
        craftButton = GetComponent<Button>();

        craftButton.onClick.AddListener(() =>
        {
            CraftingUI craftingUI = GameManager.Instance.uIManager.CloseUI<CraftingUI>();
            craftingUI = GameManager.Instance.uIManager.OpenUI<CraftingUI>();
            craftingUI.Init(craftingData);
        });
    }

    public void Init(CraftingData inCraftingData)
    {
        craftingData = inCraftingData;

        // �̹��� ������Ʈ (������ �̹����� ���Ե� ���)
        Image itemImage = GetComponentInChildren<Image>();
        Sprite sprite = Resources.Load<Sprite>(craftingData.imagePath);
        itemImage.sprite = sprite; // craftingData���� ��η� �������� �ε��Ͽ� ����
    }

}
