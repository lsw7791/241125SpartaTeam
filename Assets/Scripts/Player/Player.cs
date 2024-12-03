using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField] private string NickName;

    [SerializeField] private PlayerStats stats;  // �÷��̾��� ���� (PlayerStats)
    [SerializeField] private Inventory inventory;  // �÷��̾��� �κ��丮 (Inventory)

    // QuickSlots ������Ƽ �߰�
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot ��ü�� ����

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        stats = new PlayerStats();
        inventory = new Inventory();
        QuickSlots = new QuickSlot();  // QuickSlot ��ü�� �ʱ�ȭ
    }

    // �����͸� �����ϴ� �޼���
    public void SaveData(IPlayerRepository repository)
    {
        PlayerSaveLoad.SavePlayerData(this, repository);
        OnPlayerDataSaved?.Invoke(); // ���� �Ϸ� �� �̺�Ʈ �߻�
    }

    // �����͸� �ε��ϴ� �޼���
    public void LoadData(IPlayerRepository repository)
    {
        PlayerSaveLoad.LoadPlayerData(this, repository);
    }

    // �κ��丮 ����
    public void AddItemToInventory(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null)
    {
        inventory.AddItem(itemID, itemName, quantity, itemType, slotIndex);  // ������ �߰�
    }

    public void RemoveItemFromInventory(string itemID, int quantity)
    {
        inventory.RemoveItem(itemID, quantity);  // ������ ����
    }

    public InventoryItem GetItemFromInventory(string itemID)
    {
        return inventory.GetItem(itemID);  // ������ ��ȸ
    }

    public void TakeDamage(int damage)
    {
        stats.CurrentHP -= damage;

        if (stats.CurrentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{NickName} has died.");
        // �߰����� ���� ó�� ���� (��: ���� ���� ȭ��)
    }
    // ������Ƽ
    public PlayerStats Stats => stats;
    public Inventory Inventory => inventory;  // �κ��丮 ��ȯ
    public string PlayerNickName { get => NickName; set => NickName = value; }
}
