using UnityEngine;

public class Player : MonoSingleton<Player>,IDamageable
{
    [SerializeField] string NickName;

    public PlayerStats stats;  // 플레이어의 스탯 (PlayerStats)
    public Inventory inventory;  // 플레이어의 인벤토리 (Inventory)
    public TopDownController _topDownController;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public PlayerCamera _playerCamera;

    // QuickSlots 프로퍼티 추가
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot 객체로 변경

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;


    public Player()
    {
        stats = new PlayerStats();
        inventory = new Inventory();
        QuickSlots = new QuickSlot();  // QuickSlot 객체로 초기화
    }

    private void Awake()
    {
        _topDownController = GetComponent<TopDownController>();
    }
    // 데이터를 저장하는 메서드
    public void SaveData(IPlayerRepository repository)
    {
        PlayerSaveLoad.SavePlayerData(this, repository);
        OnPlayerDataSaved?.Invoke(); // 저장 완료 후 이벤트 발생
    }

    // 데이터를 로드하는 메서드
    public void LoadData(IPlayerRepository repository)
    {
        PlayerSaveLoad.LoadPlayerData(this, repository);
    }

    // 인벤토리 관련
    public void AddItemToInventory(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null)
    {
        inventory.AddItem(itemID, itemName, quantity, itemType, slotIndex);  // 아이템 추가
    }

    public void RemoveItemFromInventory(string itemID, int quantity)
    {
        inventory.RemoveItem(itemID, quantity);  // 아이템 제거
    }

    public InventoryItem GetItemFromInventory(string itemID)
    {
        return inventory.GetItem(itemID);  // 아이템 조회
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
        UIManager.Instance.deathUI.SetActive(true);
        Debug.Log($"{NickName} has died.");
    }
    // 프로퍼티
    public PlayerStats Stats => stats;
    public Inventory Inventory => inventory;  // 인벤토리 반환
    public string PlayerNickName { get => NickName; set => NickName = value; }
}
