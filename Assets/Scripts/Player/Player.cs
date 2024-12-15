using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] public string NickName;

    public PlayerStats stats; // �÷��̾��� ����
    public Inventory inventory; // �÷��̾��� �κ��丮
    public TopDownController _topDownController;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public PlayerCamera _playerCamera;

    // QuickSlots ������Ƽ
    public QuickSlot QuickSlots { get; private set; }

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        stats = new PlayerStats();
        stats.Initialize();
        inventory = new Inventory();
        QuickSlots = new QuickSlot(); // QuickSlot ��ü �ʱ�ȭ
    }

    private void Awake()
    {
        _topDownController = GetComponent<TopDownController>();
    }

    // �����͸� �����ϴ� �޼���
    public void SaveData(string fileName = "DefaultSave")
    {
        SaveLoadManager.SavePlayerData(this, fileName); // ���� �̸� �Ű������� ����
        OnPlayerDataSaved?.Invoke(); // ���� �Ϸ� �� �̺�Ʈ �߻�
    }

    // �����͸� �ε��ϴ� �޼���
    public void LoadData(string fileName = "DefaultSave")
    {
        SaveLoadManager.LoadPlayerData(this, fileName); // ���� �̸� �Ű������� ����
    }

    // �κ��丮 ���� �޼���: ��������Ʈ ����
    public void AddItemToInventory(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemSprite)
    {
        if (quantity <= 0)
        {
            Debug.LogWarning("��ȿ���� ���� �����Դϴ�.");
            return;
        }
        inventory.AddItem(itemID, itemName, quantity, itemType, itemSprite);
    }

    public void RemoveItemFromInventory(int itemID, int quantity)
    {
        if (quantity <= 0)
        {
            Debug.LogWarning("������ ������ ��ȿ���� �ʽ��ϴ�.");
            return;
        }
        inventory.RemoveItem(itemID, quantity);
    }

    public InventoryItem GetItemFromInventory(int itemID)
    {
        InventoryItem item = inventory.GetItem(itemID);
        if (item == null)
        {
            Debug.LogWarning($"������ ID {itemID}�� �κ��丮�� �����ϴ�.");
        }
        return item;
    }

    // ������ ó��
    public void TakeDamage(int damage)
    {
        stats.CurrentHP -= damage;

        if (stats.CurrentHP <= 0)
        {
            Die();
        }
    }

    // �÷��̾� ���� ó��
    public void Die()
    {
        _topDownController.TriggerDeath();
        Debug.Log($"{NickName} has died.");
    }

    // ������Ƽ
    public PlayerStats Stats => stats;
    public Inventory Inventory => inventory; // �κ��丮 ��ȯ
    public string PlayerNickName { get => NickName; set => NickName = value; }
}
