using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : UIBase
{
    public void OnclickedExitBtn()
    {
        UIManager.Instance.ToggleUI<OptionUI>();
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    //public void OnclickedExitBtn()
    //{
    //    SceneManager.LoadScene("TitleScene");
    //}
}
