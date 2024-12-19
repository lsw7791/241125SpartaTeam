using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("게임 시작");
        GameManager.Instance.SceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    public void LoadGame()
    {
        Debug.Log("게임 불러오기");
        UIManager.Instance.ToggleUI<CharacterSlotUI>();
    }
    //void OptionUIOn()
    //{
    //    UIManager.Instance.ToggleUI<OptionUI>();
    //}
}