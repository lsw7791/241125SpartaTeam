using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTest : MonoBehaviour
{
    public void Load()
    {
        PlayerSaveLoad.LoadPlayerData(GameManager.Instance.Player, GameManager.Instance.DataManager.Repository);
    }
}
