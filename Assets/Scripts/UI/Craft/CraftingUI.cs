using MainData;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftingUI : UIBase
{
    [SerializeField] private CraftingData craftingData;
    [SerializeField] private Image _productImage;
    [SerializeField] private TMP_Text _productText;

    [SerializeField] private Image[] _craftItemImage;
    [SerializeField] private TMP_Text[] _craftItemText;

    [SerializeField] private Button craftResultButton; // ���� ��ư

    //[SerializeField] private int _craftCount;
    //[SerializeField] private int _craftMaxCount;

    private void Start()
    {
        craftResultButton.onClick.AddListener(() =>
        {
            Debug.Log("���� ��ư Ŭ����.");

            // TryCraftItem �޼��带 ȣ���Ͽ� ������ ���� �õ�
            bool isSuccess = GameManager.Instance.CraftingManager.TryCraftItem();

            // CraftingUI �ݱ�
            UIManager.Instance.CloseUI<CraftingUI>();

            // CraftResultUI ����
            CraftResultUI craftResultUI = UIManager.Instance.OpenUI<CraftResultUI>();

            // CraftResultUI�� Sorting Order�� 3���� ����
            UIManager.Instance.SetSortingOrder<CraftResultUI>(3);

            // ��� ǥ��
            if (isSuccess)
            {
                craftResultUI.ShowSuccess(craftingData);
            }
            else
            {
                craftResultUI.ShowFailure(craftingData);
            }
        });

    }

    public void Init(CraftingData inData)
    {
        craftingData = inData;

        //_craftCount = 0;
        //_craftMaxCount = 0;

        for (int i = 0; i < _craftItemImage.Length; i++)
        {
            _craftItemImage[i].gameObject.SetActive(false);
        }

        Sprite itemSprite = Resources.Load<Sprite>(craftingData.imagePath);
        _productImage.sprite = itemSprite;
        _productText.text = craftingData.name;

        List<int> craftItemList = GameManager.Instance.DataManager.Crafting.GetCraftItemIds(craftingData.id);

        for (int i = 0; i < craftItemList.Count; i++)
        {
            _craftItemImage[i].sprite = null;
            _craftItemText[i].TryGetComponent<TMP_Text>(out var outCraftItemText);
            outCraftItemText.text = null;

            if (craftItemList[i] != 0)
            {
                //_craftMaxCount++;
                int count = GameManager.Instance.DataManager.Crafting.GetCraftCountIds(craftingData.id)[i];
                _craftItemImage[i].gameObject.SetActive(true);
                var itemData = GameManager.Instance.DataManager.GetItemDataById(craftItemList[i]);

                _craftItemImage[i].sprite = Resources.Load<Sprite>(itemData.spritePath);
                outCraftItemText.text = $"{GameManager.Instance.Player.inventory.GetItemCount(itemData.id)} / {count}\n{itemData.name}";

                //_craftCount = Craft(_craftCount, GameManager.Instance.player.inventory.GetItemCount(itemData.id), count);
            }
        }

        //craftResultButton.onClick.AddListener(() =>
        //{
        //    Debug.Log("���� ��ư Ŭ����.");

        //    // TryCraftItem �޼��带 ȣ���Ͽ� ������ ���� �õ�
        //    if (_craftMaxCount <= _craftCount)
        //    {
        //        // ���� ����
        //        GameManager.Instance.uIManager.CloseUI<CraftingUI>();
        //        CraftResultUI craftResultUI = GameManager.Instance.uIManager.OpenUI<CraftResultUI>();
        //        craftResultUI.ShowSuccess(craftingData);
        //    }
        //    else
        //    {
        //        // ���� ����
        //        GameManager.Instance.uIManager.CloseUI<CraftingUI>();
        //        CraftResultUI craftResultUI = GameManager.Instance.uIManager.OpenUI<CraftResultUI>();
        //        craftResultUI.ShowFailure(craftingData);
        //    }
        //});
    }

    //private int Craft(int inCraftResultCount, int inInventoryItemCount, int inCraftItemCount)
    //{
    //    if (inCraftItemCount <= inInventoryItemCount)
    //    {
    //        inCraftResultCount++;
    //    }
    //    return inCraftResultCount;
    //}
}
