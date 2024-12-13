using MainData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class CraftingUI : UIBase
{
    [SerializeField]
    private CraftingData craftingData;
    [SerializeField]
    private Image _productImage;
    [SerializeField]
    private TMP_Text _productText;

    [SerializeField]
    private Image[] _craftItemImage;
    [SerializeField]
    private TMP_Text[] _craftItemText;

    private Button craftResultButton; // ���� ��ư

    private void Start()
    {
        craftResultButton = GetComponent<Button>();

        craftResultButton.onClick.AddListener(() =>
        {
            CraftResultUI craftResultUI = GameManager.Instance.uIManager.OpenUI<CraftResultUI>();
            GameManager.Instance.uIManager.CloseUI<CraftingUI>();
            // ���� ��� �˾� ����
            // ���ۿ� �����Ͽ����ϴ�!
            // ���۵� ������ �̹���
            // ����� ��� �̹����� ����
            // �ݱ� ��ư
            // "30�� �� �ڵ����� â�� �����ϴ�."
            // ���� ���� UI �ڵ����� ����
            // ���۵� ������ �κ��丮�� �̵�
            // ���ۿ� �����Ͽ����ϴ�..
            // ��ᰡ ������ �� �����ϴ�.
            // ������ ��� �̹����� ����
        });
    }

    public void Init(CraftingData inData)
    {
        craftingData = inData;

        Sprite itemSprite = Resources.Load<Sprite>(craftingData.imagePath);
        _productImage.sprite = itemSprite;
        _productText.text = craftingData.name;

        List<int> craftItemList = GameManager.Instance.dataManager.crafting.GetCraftItemIds(craftingData.id);

        for(int i = 0; i < craftItemList.Count; i++)
        {
            _craftItemImage[i].sprite = null;
            _craftItemText[i].TryGetComponent<TMP_Text>(out var outCraftItemText);
            outCraftItemText.text = null;
            _craftItemImage[i].gameObject.SetActive(false);

            if (craftItemList[i] != 0)
            {
                int count = GameManager.Instance.dataManager.crafting.GetCraftCountIds(craftingData.id)[i];
                _craftItemImage[i].gameObject.SetActive(true);
                var itemData = GameManager.Instance.dataManager.GetItemDataById(craftItemList[i]);

                _craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
                outCraftItemText.text = $"{GameManager.Instance.player.inventory.GetItemCount(itemData.id)} / {count}\n{itemData.name}";
            }
        }
    }
}
