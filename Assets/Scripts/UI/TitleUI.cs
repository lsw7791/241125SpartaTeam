using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("���� ����");
        GameManager.Instance.sceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(GameManager.Instance.sceneNum));
    }
    public void LoadGame()
    {
        Debug.Log("���� �ҷ�����");
        GameManager.Instance.uIManager.ToggleUI<CharacterSlotUI>();

    }


}