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

        // ���� ������ �ε� �� �߰�
        var characters = GameManager.Instance.GetAllPlayerData();
        characters.Add(newPlayer);
        GameManager.Instance.SavePlayerData(characters);  // ������ ����

        Debug.Log($"ĳ���� {nickname} ���� �Ϸ�!");
        nicknameInputField.text = "";

    }
}
