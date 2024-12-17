using UnityEngine;
using UnityEngine.UI;

public class SaveLoadTestBtn : MonoBehaviour
{
    public Player player;  // �÷��̾� ��ü
    public Button saveButton;  // ���̺� ��ư
    public Button loadButton;  // �ε� ��ư
    private IPlayerRepository repository;  // IRepository (�ʿ�� ����)

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ���
        saveButton.onClick.AddListener(SavePlayerData);
        loadButton.onClick.AddListener(LoadPlayerData);
    }

    // ���̺� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void SavePlayerData()
    {
        player.SaveData(repository);
        Debug.Log("Player data saved.");
    }

    // �ε� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    private void LoadPlayerData()
    {
        player.LoadData(repository);
        Debug.Log("Player data loaded.");

        // �ε�� ������ ���
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
