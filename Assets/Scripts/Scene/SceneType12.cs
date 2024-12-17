using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneType12 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.SceneNum = 12;
            GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
        }
    }
}
