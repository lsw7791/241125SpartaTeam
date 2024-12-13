using MainData;
using UnityEngine;

public class ShopNPC : NPCBase
{
    public ShopType shopType; // NPC�� ���� ���� Ÿ��

    public override void Interact()
    {
        // ShopUI�� ���� �ش� ���� Ÿ�� ����
        ShopUI shopUI = GameManager.Instance.uIManager.GetUI<ShopUI>();
        shopUI.SetShopType(shopType); // ShopUI �ʱ�ȭ
    }
}
