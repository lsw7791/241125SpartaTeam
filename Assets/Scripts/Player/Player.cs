using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public string nickName;

    public PlayerData stats;  // �÷��̾��� ����
    public Inventory inventory;  // �÷��̾��� �κ��丮
    public TopDownController _topDownController;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public PlayerCamera _playerCamera;
    public Equipment equipment;

    // QuickSlots ������Ƽ
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot ��ü�� ����

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        stats = new PlayerData();
        stats.Initialize();
        inventory = new Inventory();
        QuickSlots = new QuickSlot();  // QuickSlot ��ü �ʱ�ȭ
    }

    private void Awake()
    {
        _topDownController = GetComponent<TopDownController>();
        equipment = GetComponent<Equipment>();
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

    // �κ��丮 ����: ��������Ʈ ����
    public void AddItemToInventory(int itemID, int quantity, string spritePath)
    { // ������ �κ��丮�� �߰�
        inventory.AddItem(itemID);  // ��������Ʈ ����
        PlayerSaveLoad.SavePlayerData(this, null);
    }

    public void RemoveItemFromInventory(int itemID, int quantity)
    { // ������ ������ŭ ����
        inventory.RemoveItem(itemID, quantity);  // ������ ����
    }

    public InventoryItem GetItemFromInventory(int itemID)
    { // Ư�� �������� �˻�
        return inventory.GetItem(itemID);  // ������ ��ȸ
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
        //UIManager.Instance.deathUI.SetActive(true);
        Debug.Log($"{nickName} has died.");
    }

    // ������Ƽ
    public PlayerData Stats => stats;
    public Inventory Inventory => inventory;  // �κ��丮 ��ȯ
    public string PlayerNickName { get => nickName; set => nickName = value; }
}
