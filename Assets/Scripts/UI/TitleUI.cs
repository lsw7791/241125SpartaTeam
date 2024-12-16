using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("���� ����");
        GameManager.Instance.sceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.sceneNum));
    }
    public void LoadGame()
    {
        Debug.Log("���� �ҷ�����");
        UIManager.Instance.ToggleUI<CharacterSlotUI>();
    }
}