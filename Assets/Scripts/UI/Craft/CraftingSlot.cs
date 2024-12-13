using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{ // 아이템 조합 슬롯의 UI를 관리
    private Button craftButton; // 제작 버튼
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
    }
}
