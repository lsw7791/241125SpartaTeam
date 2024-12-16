using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotUI : UIBase
{
    [SerializeField] Button _exitBtn;
    public void OnclickedExitBtn()
    {
        gameObject.SetActive(false);
    }
}
