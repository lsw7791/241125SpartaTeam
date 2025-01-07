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
        SoundManager.Instance.PlayButton2SFX();
        Debug.Log("���� ����");
        GameManager.Instance.SceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    public void LoadGame(bool inIsNewGame)
    {
        SoundManager.Instance.PlayButton2SFX();
        Debug.Log("���� �ҷ�����");
        CharacterSelectUI selectUI = UIManager.Instance.ToggleUI<CharacterSelectUI>();
        selectUI.GameStart();
    }
    public void OptionUIOn()
    {
        SoundManager.Instance.PlayButton2SFX();
        UIManager.Instance.ToggleUI<OptionUI>();
    }
    public void ExitGame()
    {
        SoundManager.Instance.PlayButton2SFX();
        // ������ ����� ȯ�濡���� �۵��ϵ��� ó��
#if UNITY_EDITOR
        // �����Ϳ��� ���� ���� ���� �÷��� ��带 �����մϴ�.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ������ ����� ȯ�濡���� ���ø����̼� ����
        Application.Quit();
#endif
    }

}