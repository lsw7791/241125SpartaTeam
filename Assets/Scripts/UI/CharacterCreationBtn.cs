using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CharacterCreationBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;

    void Awake()
    {
        var repository = GameManager.Instance.DataManager.Repository;
        if (repository is FilePlayerRepository fileRepo)
        {
            fileRepo.Initialize(); // Initialize 호출
        }
    }

    public void OnCreateCharacterButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("닉네임을 입력해주세요!");
            return;
        }

        // 캐릭터 데이터 생성
        var newPlayer = new PlayerData
        {
            NickName = nickname,
        };
        newPlayer.Initialize();  // Initialize() 호출로 기본값 설정

        // 캐릭터 리스트에 추가
        GameManager.Instance.DataManager.CharacterList.AddCharacter(newPlayer);
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
        UIManager.Instance.ToggleUI<CharacterSlotUI>();
    }

    public void Exit()
    {
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
}
