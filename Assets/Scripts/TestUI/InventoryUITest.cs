using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUITest : UIBaseTest
{
    public void CloseUI()
    {
        InventoryUITest myUI = this;

        myUI.Close();
    }

    private void ClearList()
    {
        
    }

    protected override void CloseProcedure()
    {
        ClearList();
    }
}
