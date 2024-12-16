using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterCreationBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField; // 닉네임 입력

    public void OnCreateCharacterButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("닉네임을 입력해주세요!");
            return;
        }

        // 새로운 캐릭터 데이터 생성
        PlayerData newPlayer = new PlayerData
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
            QuickSlotItems = new List<QuickSlotItem>(), // 초기화
            Stats = new PlayerStatsData() // 기본 스탯 데이터
        };

        // 슬롯에 추가
        GameManager.Instance.slotManager.AddCharacterToSlot(newPlayer);

        // 입력 필드 초기화
        nicknameInputField.text = "";

        Debug.Log($"캐릭터 {nickname} 생성 완료!");

        // **GameManager 초기화**
        if (GameManager.Instance == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }

        GameManager.Instance.sceneNum = 23;
        GameManager.Instance.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(GameManager.Instance.sceneNum));

    }

}
