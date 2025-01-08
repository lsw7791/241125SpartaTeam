using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;
using System.Collections.Generic;
using UnityEngine.U2D;

public class CraftResultUI : UIBase
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _messageText;

    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _productText;

    [SerializeField] private Image[] _craftItemImage;
    [SerializeField] private TMP_Text[] _craftItemText;
    private float _maxTime;
    private void Update()
    {
        if(_maxTime > 0)
        {
            _maxTime -= Time.deltaTime;

            _messageText.text = $"{(int)_maxTime}초 후 자동으로 창이 닫힙니다.";
        }
        else
        {
            UIManager.Instance.CloseUI<CraftResultUI>();
        }
    }

    public void ShowSuccess(CraftingData data)
    {
        _maxTime = 10;

        _resultText.text = "제작에 성공하였습니다!";
        
        GameManager.Instance.CraftingManager.AddToInventory();
        Init(data, true);
        // Dictionary에서 QuestID 1에 해당하는 값 확인
        if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(2) &&
            !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[2])
        {
            // QuestCompletionStatus[1]이 false일 때만 CompleteQuest(1) 호출
            GameManager.Instance.DataManager.MainQuest.CompleteQuest(2);
        }

    }

    public void ShowFailure(CraftingData data)
    {
        _maxTime = 10;

        _resultText.text = "제작에 실패하였습니다.\n부족한 재료를 확인해 주세요.";

        Init(data, false);
    }

    public void Init(CraftingData inData, bool inResult)
    {
        for (int i = 0; i < _craftItemImage.Length; i++)
        {
            _craftItemImage[i].gameObject.SetActive(false);
        }

        //Sprite itemSprite = Resources.Load<Sprite>(inData.imagePath);
        Sprite itemSprite = UIManager.Instance.craftingAtlas.GetSprite(inData.atlasPath);
        _productImage.sprite = itemSprite;
        _productText.text = inData.name;

        List<int> craftItemList = GameManager.Instance.DataManager.Crafting.GetCraftItemIds(inData.id);

        for (int i = 0; i < craftItemList.Count; i++)
        {
            if (craftItemList[i] == 0)
            {
                return;
            }

            _craftItemImage[i].gameObject.SetActive(true);
            var itemData = GameManager.Instance.DataManager.GetItemDataById(craftItemList[i]);

            //_craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
            if(inData.id == 61)
            {
                _craftItemImage[i].sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
            }
            else
            {
                _craftItemImage[i].sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
            }
            //_craftItemImage[i].sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
            int craftItemCount = GameManager.Instance.DataManager.Crafting.GetCraftCountIds(inData.id)[i];
            int inventoryItemCount = GameManager.Instance.Player.inventory.GetItemCount(itemData.id);

            if (inResult)
            {
                _craftItemText[i].text = $"{inventoryItemCount} / {craftItemCount}\n{itemData.name}";
            }
            else
            {
                _craftItemText[i].text = $"{craftItemCount - inventoryItemCount}개 부족\n{itemData.name}";
            }
        }
    }
}
