using MainData;
using UnityEngine;

public class ShopNPC : NPCBase
{
    public ShopType ShopType; // NPC가 가진 상점 타입

    public override void Interact()
    {
        // ShopUI를 열고 해당 상점 타입 전달
        ShopUI shopUI = UIManager.Instance.GetUI<ShopUI>();
        UIManager.Instance.ToggleUI<ShopUI>();
        shopUI.SetShopType(ShopType); // ShopUI 초기화
    }
}
