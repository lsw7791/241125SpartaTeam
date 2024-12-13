using MainData;
using UnityEngine;

public class PotionNPC : ShopNPC
{    private void Awake()
    {
        shopType = ShopType.PotionShop;  // PotionShop 타입으로 설정
    }

    public override void Interact()
    {
        base.Interact();  // ShopUI를 열 때 PotionShop 타입을 전달
    }
}
