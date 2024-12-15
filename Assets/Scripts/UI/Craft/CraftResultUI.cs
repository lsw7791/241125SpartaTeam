using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;
using System.Collections.Generic;

public class CraftResultUI : UIBase
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Image resultImage;
    [SerializeField] private TMP_Text messageText;

    [SerializeField] private CraftingData craftingData;

    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _productText;

    [SerializeField] private Image[] _craftItemImage;
    [SerializeField] private TMP_Text[] _craftItemText;

    private float maxTime;

    private void Update()
    {
        if(maxTime > 0)
        {
            maxTime -= Time.deltaTime;

            messageText.text = $"{(int)maxTime}�� �� �ڵ����� â�� �����ϴ�.";
        }
        else
        {
            GameManager.Instance.uIManager.CloseUI<CraftResultUI>();
        }
    }

    public void ShowSuccess(CraftingData data)
    {
        maxTime = 10;

        resultText.text = "���ۿ� �����Ͽ����ϴ�!";
        // �������� �κ��丮�� �̵��ϴ� ���� �ʿ�
        // ��: �κ��丮 �߰�

        Init(data, true);
    }

    public void ShowFailure(CraftingData data)
    {
        maxTime = 10;

        resultText.text = "���ۿ� �����Ͽ����ϴ�.\n������ ��Ḧ Ȯ���� �ּ���.";

        Init(data, false);
    }

    public void Init(CraftingData inData, bool inResult)
    {
        craftingData = inData;

        for (int i = 0; i < _craftItemImage.Length; i++)
        {
            _craftItemImage[i].gameObject.SetActive(false);
        }

        Sprite itemSprite = Resources.Load<Sprite>(craftingData.imagePath);
        _productImage.sprite = itemSprite;
        _productText.text = craftingData.name;

        List<int> craftItemList = GameManager.Instance.dataManager.crafting.GetCraftItemIds(craftingData.id);

        for (int i = 0; i < craftItemList.Count; i++)
        {
            _craftItemImage[i].sprite = null;
            _craftItemText[i].text = null;

            if (craftItemList[i] != 0)
            {
                int count = GameManager.Instance.dataManager.crafting.GetCraftCountIds(craftingData.id)[i];
                _craftItemImage[i].gameObject.SetActive(true);
                var itemData = GameManager.Instance.dataManager.GetItemDataById(craftItemList[i]);

                _craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);

                if (inResult)
                {
                    _craftItemText[i].text = $"{GameManager.Instance.player.inventory.GetItemCount(itemData.id)} / {count}\n{itemData.name}";
                }
                else
                {
                    _craftItemText[i].text = $"{count -  GameManager.Instance.player.inventory.GetItemCount(itemData.id)}�� ����\n{itemData.name}";
                }
            }
        }
    }
}
