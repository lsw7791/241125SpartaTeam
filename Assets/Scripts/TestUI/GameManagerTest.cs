using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTest : MonoSingleton<GameManagerTest>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //UIManagerTest.Instance.Init();

    }
}
