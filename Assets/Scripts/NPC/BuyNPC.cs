using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNPC : ShopNPC
{
    private void Awake()
    {
        ShopType = ShopType.BuyShop;  // PotionShop 타입으로 설정
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI를 열 때 PotionShop 타입을 전달
    }
}
