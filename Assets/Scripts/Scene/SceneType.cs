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
            string currentSceneName = SceneManager.GetActiveScene().name;//���� �� �̸� ��������

            if (currentSceneName != GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum))
            {
                // ���̵� ȿ���� �� �ε�
                UIManager.Instance.fadeManager.LoadSceneWithFade(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
                //GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));

            }
            else
            {
                Camera mainCamera = Camera.main;
                mainCamera.backgroundColor = Color.black;
                mainCamera.transform.position = GameManager.Instance.DataManager.Scene.GetMoveTransform(num);
                Transform playerTransform = collision.transform; // �浹�� �÷��̾��� Transform ��������
                playerTransform.position = mainCamera.transform.position;
                GameManager.Instance.miniCamera.transform.position = mainCamera.transform.position;
            }
        }
    }
}
