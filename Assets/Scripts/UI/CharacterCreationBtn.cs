using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CharacterCreationBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField;

    void Awake()
    {
            var repository = GameManager.Instance.repository;
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

        // 기존 데이터 로드 후 추가
        var characters = GameManager.Instance.GetAllPlayerData();
        characters.Add(newPlayer);
        GameManager.Instance.SavePlayerData(characters);  // 데이터 저장

        Debug.Log($"캐릭터 {nickname} 생성 완료!");
        nicknameInputField.text = "";

    }
}
