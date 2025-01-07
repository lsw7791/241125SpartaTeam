using UnityEngine;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnCreateCharacterButtonClicked()
    {
        DataManager dataManager = GameManager.Instance.DataManager;

        string nickname = nicknameInputField.text;

        var newPlayer = new PlayerData
        {
            NickName = nickname,
        };
        GameManager.Instance.nowPlayer = newPlayer;
        GameManager.Instance.nowPlayer.Initialize();

        dataManager.SaveData(newPlayer);
        dataManager.SaveData(new Inventory());
        //dataManager.SaveData(new Equipment());
        GameManager.Instance.StartGame();
    }

    public void Exit()
    {
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
}