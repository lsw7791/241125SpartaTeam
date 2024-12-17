using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class CharacterCreationBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;

    void Awake()
    {
            var repository = GameManager.Instance.Repository;
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
            MaxHP = 100,
            CurrentHP = 100,
            CurrentStamina = 50,
            CurrentGold = 0,
            CurrentDamage = 10,
            CurrentSpeed = 5,
            CurrentATKSpeed = 1.0f,
            CurrentDef = 5,
            CurrentWeaponType = 1,
            QuickSlotItems = new List<QuickSlotItem>(),
            Stats = new PlayerStatsData()
        };

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

        // 슬롯 UI 갱신
    

}
