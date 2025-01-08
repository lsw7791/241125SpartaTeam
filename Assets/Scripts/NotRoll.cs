using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRoll : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.Player.playerRoll.canRoll = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.Instance.Player.playerRoll.canRoll = true;
    }
}
