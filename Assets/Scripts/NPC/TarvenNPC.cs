using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarvenNPC : ShopNPC
{
    private void Awake()
    {
        ShopType = ShopType.TarvenShop;  // WeaponShop Ÿ������ ����
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI�� �� �� WeaponShop Ÿ���� ����
    }
}
