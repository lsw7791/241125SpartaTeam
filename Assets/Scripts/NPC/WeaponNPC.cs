using MainData;
using UnityEngine;

public class WeaponNPC : ShopNPC
{
    private void Awake()
    {
        ShopType = ShopType.WeaponShop;  // WeaponShop Ÿ������ ����
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI�� �� �� WeaponShop Ÿ���� ����
    }
}
