using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : UIBase
{
    public GameObject InfoBackground;
    public GameObject InputBackground;

    public void CloseInfoMain()
    {
        gameObject.SetActive(false);
    }
    public void GameInfoOpen()
    {
        InfoBackground.gameObject.SetActive(true);
    }
    public void GameInfoClose()
    {
        InfoBackground.gameObject.SetActive(false);

    }
    public void GameInputOpen()
    {
        InputBackground.gameObject.SetActive(true);

    }
    public void GameInputClose()
    {
        InputBackground.gameObject.SetActive(false);
    }
}
