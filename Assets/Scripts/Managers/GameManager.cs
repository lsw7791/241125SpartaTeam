using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
      
        //DataManager.Instance.LoadData();

    }
    private void Start()
    {
    }
}
