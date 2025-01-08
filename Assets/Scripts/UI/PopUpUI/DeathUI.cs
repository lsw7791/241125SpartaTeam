using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : UIBase
{
    public void ReStart()
    {
        GameManager.Instance.Player.Revive();
    }
}
