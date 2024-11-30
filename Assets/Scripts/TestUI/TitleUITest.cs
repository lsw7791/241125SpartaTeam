using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUITest : UIBaseTest
{
    public void GameStart()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene("TestInGameSceen");
    }
}
