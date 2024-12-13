using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{ // 아이템 조합 슬롯의 UI를 관리
    private Button craftButton; // 크래프트 버튼
    private CraftingData craftingData;

    private void Start()
    {
        craftButton = GetComponent<Button>();

        craftButton.onClick.AddListener(() =>
        {
            CraftingUI craftingUI = GameManager.Instance.uIManager.OpenUI<CraftingUI>();
            craftingUI.Init(craftingData);
        });
    }

    public void Init(CraftingData inCraftingData)
    {
        craftingData = inCraftingData;
    }

    //// 크래프트 버튼 클릭 시 처리하는 메서드
    //public void OnCraftButtonClick()
    //{ // 버튼 클릭 시, GameManager의 CraftingManager를 통해 아이템 선택을 처리
    //    // CraftingManager에서 아이템 선택 및 크래프트 시도
    //    if (craftingData != null)
    //    {
    //        GameManager.Instance.craftingManager.SelectItem(craftingData.id); // 아이템 선택
    //    }
    //}
    //// 현재 구현은 아이템 선택 단계
}
