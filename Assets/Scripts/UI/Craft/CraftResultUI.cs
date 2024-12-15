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

            messageText.text = $"{(int)maxTime}초 후 자동으로 창이 닫힙니다.";
        }
        else
        {
            GameManager.Instance.uIManager.CloseUI<CraftResultUI>();
        }
    }

    public void ShowSuccess(CraftingData data)
    {
        maxTime = 10;

        resultText.text = "제작에 성공하였습니다!";
        // 아이템을 인벤토리로 이동하는 로직 필요
        // 예: 인벤토리 추가

        Init(data, true);
    }

    public void ShowFailure(CraftingData data)
    {
        maxTime = 10;

        resultText.text = "제작에 실패하였습니다.\n부족한 재료를 확인해 주세요.";

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
                    _craftItemText[i].text = $"{count -  GameManager.Instance.player.inventory.GetItemCount(itemData.id)}개 부족\n{itemData.name}";
                }
            }
        }
    }
}
