using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("���� ����");
        SceneManager.LoadScene("CharacterCreation");
    }
    public void LoadGame()
    {
        Debug.Log("���� �ҷ�����");
        GameManager.Instance.uIManager.ToggleUI<CharacterSlotUI>();

    }


}