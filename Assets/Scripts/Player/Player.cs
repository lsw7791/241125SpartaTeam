using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [SerializeField] string NickName;

    [SerializeField] PlayerStats stats;  // �÷��̾��� ���� (PlayerStats)
    [SerializeField] Inventory inventory;  // �÷��̾��� �κ��丮 (Inventory)
    [SerializeField] TopDownController _topDownController;

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

    private void Awake()
    {
        _topDownController = GetComponent<TopDownController>();
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
        _topDownController.TriggerDeath();
        Debug.Log($"{NickName} has died.");
    }
    // ������Ƽ
    public PlayerStats Stats => stats;
    public Inventory Inventory => inventory;  // �κ��丮 ��ȯ
    public string PlayerNickName { get => NickName; set => NickName = value; }
}
