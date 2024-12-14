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

        // 이미지 업데이트 (아이템 이미지가 포함된 경우)
        Image itemImage = GetComponentInChildren<Image>();
        Sprite sprite = Resources.Load<Sprite>(craftingData.imagePath);
        itemImage.sprite = sprite; // craftingData에서 경로로 아이콘을 로드하여 설정
    }

}
