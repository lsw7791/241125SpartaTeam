using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public string nickName;

    public PlayerStats stats;  // 플레이어의 스탯
    public Inventory inventory;  // 플레이어의 인벤토리
    public TopDownController _topDownController;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public PlayerCamera _playerCamera;
    public Equipment equipment;

    // QuickSlots 프로퍼티
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot 객체로 변경

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        stats = new PlayerStats();
        stats.Initialize();
        inventory = new Inventory();
        QuickSlots = new QuickSlot();  // QuickSlot 객체 초기화
    }

    private void Awake()
    {
        _topDownController = GetComponent<TopDownController>();
        equipment = GetComponent<Equipment>();
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

    // 인벤토리 관련: 스프라이트 포함
    public void AddItemToInventory(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemSprite)
    { // 이템을 인벤토리에 추가
        inventory.AddItem(itemID, itemName, quantity, itemType, itemSprite);  // 스프라이트 포함
        PlayerSaveLoad.SavePlayerData(this, null);
    }

    public void RemoveItemFromInventory(int itemID, int quantity)
    { // 지정된 수량만큼 제거
        inventory.RemoveItem(itemID, quantity);  // 아이템 제거
    }

    public InventoryItem GetItemFromInventory(int itemID)
    { // 특정 아이템을 검색
        return inventory.GetItem(itemID);  // 아이템 조회
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
        //UIManager.Instance.deathUI.SetActive(true);
        Debug.Log($"{nickName} has died.");
    }

    // 프로퍼티
    public PlayerStats Stats => stats;
    public Inventory Inventory => inventory;  // 인벤토리 반환
    public string PlayerNickName { get => nickName; set => nickName = value; }
}
