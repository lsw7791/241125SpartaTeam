using MainData;
using UnityEngine;

public class WeaponNPC : ShopNPC
{
    private void Awake()
    {
        ShopType = ShopType.WeaponShop;  // WeaponShop 타입으로 설정
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI를 열 때 WeaponShop 타입을 전달
    }
}
