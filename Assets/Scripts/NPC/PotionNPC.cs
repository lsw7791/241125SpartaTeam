using MainData;
using UnityEngine;

public class PotionNPC : ShopNPC
{    private void Awake()
    {
        shopType = ShopType.PotionShop;  // PotionShop Ÿ������ ����
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI�� �� �� PotionShop Ÿ���� ����
    }
}
