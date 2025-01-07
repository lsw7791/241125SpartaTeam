using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : UIBase
{
    public GameObject InfoBackground;
    public GameObject InputBackground;

    public void CloseInfoMain()
    {
        SoundManager.Instance.PlayButton1SFX();

        gameObject.SetActive(false);
    }
    public void GameInfoOpen()
    {
        SoundManager.Instance.PlayButton1SFX();

        InfoBackground.gameObject.SetActive(true);
    }
    public void GameInfoClose()
    {
        SoundManager.Instance.PlayButton1SFX();

        InfoBackground.gameObject.SetActive(false);

    }
    public void GameInputOpen()
    {
        SoundManager.Instance.PlayButton1SFX();

        InputBackground.gameObject.SetActive(true);

    }
    public void GameInputClose()
    {
        SoundManager.Instance.PlayButton1SFX();

        InputBackground.gameObject.SetActive(false);
    }
}
