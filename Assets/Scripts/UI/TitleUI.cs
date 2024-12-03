using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void GameStart()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene("InGameScene");
    }
}