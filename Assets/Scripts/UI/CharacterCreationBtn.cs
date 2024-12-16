using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterCreationBtn : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInputField; // �г��� �Է�

    public void OnCreateCharacterButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("�г����� �Է����ּ���!");
            return;
        }

        // ���ο� ĳ���� ������ ����
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
            QuickSlotItems = new List<QuickSlotItem>(), // �ʱ�ȭ
            Stats = new PlayerStatsData() // �⺻ ���� ������
        };

        // ���Կ� �߰�
        GameManager.Instance.slotManager.AddCharacterToSlot(newPlayer);

        // �Է� �ʵ� �ʱ�ȭ
        nicknameInputField.text = "";

        Debug.Log($"ĳ���� {nickname} ���� �Ϸ�!");

        // **GameManager �ʱ�ȭ**
        if (GameManager.Instance == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
        }

        GameManager.Instance.sceneNum = 23;
        GameManager.Instance.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(GameManager.Instance.sceneNum));

    }

}
