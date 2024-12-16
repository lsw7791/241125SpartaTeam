using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : UIBase
{
    public void OnclickedExitBtn()
    {
        GameManager.Instance.sceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.sceneNum));
    }
    //public void OnclickedExitBtn()
    //{
    //    SceneManager.LoadScene("TitleScene");
    //}
}
