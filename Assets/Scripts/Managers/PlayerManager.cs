using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData PlayerData { get; private set; }

    private void Start()
    {
        // 게임 시작 시 저장된 데이터를 불러옴
        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        SaveLoadManager.SavePlayerData(PlayerData);
    }

    public void LoadPlayerData()
    {
        PlayerData = SaveLoadManager.LoadPlayerData();
    }
}