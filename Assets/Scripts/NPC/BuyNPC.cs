using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyNPC : ShopNPC
{
    private void Awake()
    {
        ShopType = ShopType.BuyShop;  // PotionShop Ÿ������ ����
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI�� �� �� PotionShop Ÿ���� ����
    }
}
