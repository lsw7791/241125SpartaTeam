using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneType2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.sceneNum = 2;
            GameManager.Instance.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(GameManager.Instance.sceneNum));
        }
    }
}
