using UnityEngine;
using UnityEngine.UI;

public class SaveLoadTestBtn : MonoBehaviour
{
    public Player player;  // 플레이어 객체
    public Button saveButton;  // 세이브 버튼
    public Button loadButton;  // 로드 버튼
    private IPlayerRepository repository;  // IRepository (필요시 구현)

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        saveButton.onClick.AddListener(SavePlayerData);
        loadButton.onClick.AddListener(LoadPlayerData);
    }

    // 세이브 버튼 클릭 시 호출되는 메서드
    private void SavePlayerData()
    {
        player.SaveData(repository);
        Debug.Log("Player data saved.");
    }

    // 로드 버튼 클릭 시 호출되는 메서드
    private void LoadPlayerData()
    {
        player.LoadData(repository);
        Debug.Log("Player data loaded.");

        // 로드된 데이터 출력
        Debug.Log($"NickName: {player.PlayerNickName}");
        Debug.Log($"HP: {player.Stats.CurrentHP}, Stamina: {player.Stats.CurrentStamina}");
        Debug.Log($"Gold: {player.Stats.Gold}");
        Debug.Log($"Inventory items: {player.Inventory.Items.Count}");
        foreach (var item in player.Inventory.Items)
        {
            Debug.Log($"Item: {item.ItemName}, Quantity: {item.Quantity}");
        }
    }
}
