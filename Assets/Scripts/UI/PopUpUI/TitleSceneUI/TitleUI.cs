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
            targetImage[i].sprite = UIManager.Instance.UIAtlas.GetSprite(GameManager.Instance.DataManager.UIDataManager.GetAtlasData(3));  // 새로운 스프라이트로 변경
        }
    }
    public void StartGame()
    {
        Debug.Log("게임 시작");
        GameManager.Instance.SceneNum = 24;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    public void LoadGame(bool inIsNewGame)
    {
        Debug.Log("게임 불러오기");
        CharacterSelectUI selectUI = UIManager.Instance.ToggleUI<CharacterSelectUI>();
        selectUI.isNewGame = inIsNewGame;
    }
    public void OptionUIOn()
    {
        UIManager.Instance.ToggleUI<OptionUI>();
    }
    public void ExitGame()
    {
        // 게임이 빌드된 환경에서만 작동하도록 처리
#if UNITY_EDITOR
        // 에디터에서 실행 중일 때는 플레이 모드를 중지합니다.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 게임이 빌드된 환경에서는 애플리케이션 종료
        Application.Quit();
#endif
    }

}