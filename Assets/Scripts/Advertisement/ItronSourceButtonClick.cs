using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItronSourceButtonClick : MonoBehaviour
{
    public void ShowBannerAd()
    {
        IronSourceInitializer.Instance.ShowBannerAd();
        GameManager.Instance.Player.stats.Gold += 1000;
    }
}
