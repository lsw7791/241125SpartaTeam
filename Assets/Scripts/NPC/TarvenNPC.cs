using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarvenNPC : ShopNPC
{
    private void Awake()
    {
        ShopType = ShopType.TarvenShop;  // WeaponShop 타입으로 설정
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI를 열 때 WeaponShop 타입을 전달
    }
}
