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

    private Button craftResultButton; // 조합 버튼

    private void Start()
    {
        craftResultButton = GetComponent<Button>();

        craftResultButton.onClick.AddListener(() =>
        {
            CraftResultUI craftResultUI = GameManager.Instance.uIManager.OpenUI<CraftResultUI>();
            GameManager.Instance.uIManager.CloseUI<CraftingUI>();
            // 제작 결과 팝업 실행
            // 제작에 성공하였습니다!
            // 제작된 아이템 이미지
            // 사용한 재료 이미지와 갯수
            // 닫기 버튼
            // "30초 후 자동으로 창이 닫힙니다."
            // 현재 제작 UI 자동으로 닫힘
            // 제작된 아이템 인벤토리로 이동
            // 제작에 실패하였습니다..
            // 재료가 부족한 것 같습니다.
            // 부족한 재료 이미지와 갯수
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
