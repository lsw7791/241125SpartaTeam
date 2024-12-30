using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReward : MonoBehaviour
{
    public void Button1000Gold()
    {
        gameObject.SetActive(false);
        GameManager.Instance.rewardedAds.ShowAd();
        gameObject.SetActive(true);
        GameManager.Instance.Player.stats.Gold += 1000;

    }
}
