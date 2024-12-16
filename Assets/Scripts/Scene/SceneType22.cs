using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneType22 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.sceneNum = 22;
            GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.sceneNum));
        }
    }
}
