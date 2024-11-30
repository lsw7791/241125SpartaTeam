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

        if (SceneManager.GetActiveScene().name == "TestTitleSceen")
        {
            UIManagerTest.Instance.Show<TitleUITest>();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TestInGameSceen")
        {
            UIManagerTest.Instance.Show<InGameUITest>();
        }
        else if (scene.name == "TestTitleSceen")
        {
            UIManagerTest.Instance.Show<TitleUITest>();
        }
    }
}
