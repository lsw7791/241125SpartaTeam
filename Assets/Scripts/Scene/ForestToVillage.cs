using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestToVillage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(2));
        }
    }
}
