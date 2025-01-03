using UnityEngine;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnCreateCharacterButtonClicked()
    {
        string nickname = nicknameInputField.text;

        var newPlayer = new PlayerData
        {
            NickName = nickname,
        };
        GameManager.Instance.DataManager.nowPlayer = newPlayer;
        GameManager.Instance.DataManager.nowPlayer.Initialize();
        GameManager.Instance.DataManager.SaveData();
        GameManager.Instance.StartGame(newPlayer);
    }

    public void Exit()
    {
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
}
