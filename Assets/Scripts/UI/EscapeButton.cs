using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButton : MonoBehaviour
{
    public void PlayerEscape()
    {
        Vector2 Playerpostion = GameManager.Instance.DataManager.Scene.GetMoveTransform(GameManager.Instance.SceneNum);
        GameManager.Instance.Player.gameObject.transform.position = Playerpostion;
    }
}
