using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPCBase
{
    public override void Interact()
    {
        GameManager.Instance.uIManager.ToggleUI<ShopUI>();
    }

}
