using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("게임 시작");
        GameManager.Instance.sceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(GameManager.Instance.sceneNum));
    }
    public void LoadGame()
    {
        Debug.Log("게임 불러오기");
        GameManager.Instance.uIManager.ToggleUI<CharacterSlotUI>();

    }


}