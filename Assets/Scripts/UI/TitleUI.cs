using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void GameStart()
    {
        Debug.Log("���� ����");
        SceneManager.LoadScene("InGameScene");
    }
}