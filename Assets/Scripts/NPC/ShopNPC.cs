using MainData;
using UnityEngine;

public class ShopNPC : NPCBase
{
    public ShopType ShopType; // NPC�� ���� ���� Ÿ��

    public override void Interact()
    {
        // ShopUI�� ���� �ش� ���� Ÿ�� ����
        ShopUI shopUI = UIManager.Instance.GetUI<ShopUI>();
        UIManager.Instance.ToggleUI<ShopUI>();
        shopUI.SetShopType(ShopType); // ShopUI �ʱ�ȭ
    }
}
