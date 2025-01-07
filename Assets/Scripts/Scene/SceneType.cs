using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneType : MonoBehaviour
{
    [SerializeField]private int num;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.Player.StopRolling();
            GameManager.Instance.SceneNum = num;
            string currentSceneName = SceneManager.GetActiveScene().name;//현재 씬 이름 가져오기
            if(currentSceneName != GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum))
            {
            GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
            }
            else
            {

                Transform playerTransform = collision.transform; // 충돌한 플레이어의 Transform 가져오기
                Camera mainCamera = Camera.main;
                mainCamera.backgroundColor = Color.black;
                mainCamera.transform.position = GameManager.Instance.DataManager.Scene.GetMoveTransform(num);
                playerTransform.position = mainCamera.transform.position;
                GameManager.Instance.miniCamera.transform.position = mainCamera.transform.position;
            }
        }
    }
}
