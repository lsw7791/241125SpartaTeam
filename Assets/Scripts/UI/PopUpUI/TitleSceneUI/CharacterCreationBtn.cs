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
            fileRepo.Initialize(); // Initialize ȣ��
        }
    }

    public void OnCreateCharacterButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("�г����� �Է����ּ���!");
            return;
        }

        // ĳ���� ������ ����
        var newPlayer = new PlayerData
        {
            NickName = nickname,
        };
        newPlayer.Initialize();  // Initialize() ȣ��� �⺻�� ����

        // ĳ���� ����Ʈ�� �߰�
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
