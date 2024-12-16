using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene("CharacterCreation");
    }
    public void LoadGame()
    {
        Debug.Log("게임 불러오기");
        GameManager.Instance.uIManager.ToggleUI<CharacterSlotUI>();

    }


}