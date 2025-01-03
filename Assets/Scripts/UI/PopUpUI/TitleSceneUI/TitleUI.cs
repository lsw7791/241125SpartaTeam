using MainData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleUI : MonoBehaviour
{

    private void Start()
    {

        Image[] targetImage  = GetComponentsInChildren<Image>();

        for(int i=0;i< targetImage.Length;i++)
        {
            targetImage[i].sprite = UIManager.Instance.UIAtlas.GetSprite(GameManager.Instance.DataManager.UIDataManager.GetAtlasData(3));  // ���ο� ��������Ʈ�� ����
        }
    }
    public void StartGame()
    {
        Debug.Log("���� ����");
        GameManager.Instance.SceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    public void LoadGame(bool inIsNewGame)
    {
        Debug.Log("���� �ҷ�����");
        CharacterSelectUI selectUI = UIManager.Instance.ToggleUI<CharacterSelectUI>();
        selectUI.isNewGame = inIsNewGame;
    }
    public void OptionUIOn()
    {
        UIManager.Instance.ToggleUI<OptionUI>();
    }
}