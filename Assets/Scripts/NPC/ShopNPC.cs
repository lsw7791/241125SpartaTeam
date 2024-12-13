using MainData;
using UnityEngine;

public class ShopNPC : NPCBase
{
    public ShopType shopType; // NPC가 가진 상점 타입

    public override void Interact()
    {
        // ShopUI를 열고 해당 상점 타입 전달
        ShopUI shopUI = GameManager.Instance.uIManager.GetUI<ShopUI>();
        GameManager.Instance.uIManager.ToggleUI<ShopUI>();
        shopUI.SetShopType(shopType); // ShopUI 초기화
    }
}
