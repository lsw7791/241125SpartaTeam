using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] public string NickName;

    public PlayerStats stats; // 플레이어의 스탯
    public Inventory inventory; // 플레이어의 인벤토리
    public TopDownController _topDownController;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public PlayerCamera _playerCamera;

    // QuickSlots 프로퍼티
    public QuickSlot QuickSlots { get; private set; }

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        stats = new PlayerStats();
        stats.Initialize();
        inventory = new Inventory();
        QuickSlots = new QuickSlot(); // QuickSlot 객체 초기화
    }

    private void Awake()
    {
        _topDownController = GetComponent<TopDownController>();
    }

    // 데이터를 저장하는 메서드
    public void SaveData(string fileName = "DefaultSave")
    {
        SaveLoadManager.SavePlayerData(this, fileName); // 파일 이름 매개변수로 전달
        OnPlayerDataSaved?.Invoke(); // 저장 완료 후 이벤트 발생
    }

    // 데이터를 로드하는 메서드
    public void LoadData(string fileName = "DefaultSave")
    {
        SaveLoadManager.LoadPlayerData(this, fileName); // 파일 이름 매개변수로 전달
    }

    // 인벤토리 관련 메서드: 스프라이트 포함
    public void AddItemToInventory(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemSprite)
    {
        if (quantity <= 0)
        {
            Debug.LogWarning("유효하지 않은 수량입니다.");
            return;
        }
        inventory.AddItem(itemID, itemName, quantity, itemType, itemSprite);
    }

    public void RemoveItemFromInventory(int itemID, int quantity)
    {
        if (quantity <= 0)
        {
            Debug.LogWarning("제거할 수량이 유효하지 않습니다.");
            return;
        }
        inventory.RemoveItem(itemID, quantity);
    }

    public InventoryItem GetItemFromInventory(int itemID)
    {
        InventoryItem item = inventory.GetItem(itemID);
        if (item == null)
        {
            Debug.LogWarning($"아이템 ID {itemID}가 인벤토리에 없습니다.");
        }
        return item;
    }

    // 데미지 처리
    public void TakeDamage(int damage)
    {
        stats.CurrentHP -= damage;

        if (stats.CurrentHP <= 0)
        {
            Die();
        }
    }

    // 플레이어 죽음 처리
    public void Die()
    {
        _topDownController.TriggerDeath();
        Debug.Log($"{NickName} has died.");
    }

    // 프로퍼티
    public PlayerStats Stats => stats;
    public Inventory Inventory => inventory; // 인벤토리 반환
    public string PlayerNickName { get => NickName; set => NickName = value; }
}
