using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("���� ����");
        SceneManager.LoadScene("PlayerCreation");
    }
    public void LoadGame()
    {
        Debug.Log("���� �ҷ�����");
        SceneManager.LoadScene("InGameScene");

    }


}