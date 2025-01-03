using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public string nickName;

    public PlayerData stats;  // 플레이어의 스탯
    public Inventory inventory;  // 플레이어의 인벤토리
    public PlayerInput PlayerInput;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public Equipment equipment;
    public GameObject Weapon;
    public ConditionUI ConditionUI;
    public StatusUI StatusUI;
    private float staminaRechargeTimer = 0f;
    // QuickSlots 프로퍼티
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot 객체로 변경

    public delegate void PlayerDataSavedHandler();
    //public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        inventory = new Inventory();
        stats = GameManager.Instance.DataManager.nowPlayer;
        QuickSlots = new QuickSlot();  // QuickSlot 객체 초기화

    }

    private void Awake()
    {
        //stats.Initialize();
        PlayerInput = GetComponent<PlayerInput>();
        equipment = GetComponent<Equipment>();
    }

    private void FixedUpdate()
    {
        // FixedUpdate의 시간 간격은 Time.fixedDeltaTime으로 고정되어 있음
        staminaRechargeTimer += Time.fixedDeltaTime;

        // 1초마다 실행
        if (staminaRechargeTimer >= 1f)
        {
            if (stats.MaxStamina > stats.CurrentStamina)
            {
                stats.CurrentStamina += 5;
                ConditionUI._stamina.value = stats.CurrentStamina; // UI 업데이트
            }
            staminaRechargeTimer = 0f; // 타이머 초기화
        }
    }
    // 인벤토리 관련: 스프라이트 포함
    public void AddItemToInventory(int itemID, int quantity, string spritePath)
    { // 이템을 인벤토리에 추가
        inventory.AddItem(itemID, 1);  // 스프라이트 포함
    }

    // 데미지 처리
    public void TakeDamage(int damage)
    {
        int value = stats.Def;
        value -= damage;
        if(value <0)
        {
            stats.CurrentHP += value;
            ConditionUI.UpdateSliders();
            if (stats.CurrentHP <= 0)
            {
                Die();
            }
        }  
    }

    // 플레이어 죽음 처리
    public void Die()
    {
        TriggerDeath();
        //UIManager.Instance.deathUI.SetActive(true);
        Debug.Log($"{nickName} has died.");
        Revive();
        GameManager.Instance.SceneNum = 2;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    public void TriggerDeath()
    {
        GameManager.Instance.Player.stats.isDie = true;
        PlayerInput.speed = 0f;
        GameManager.Instance.Player._playerAnimationController.TriggerDeathAnimation(); // 죽음 애니메이션 실행
        this.enabled = false;

    }
    public void Revive()
    {
        stats.isDie = false;
        stats.CurrentHP = 20;
        ConditionUI.UpdateSliders();
        PlayerInput.speed = stats.MoveSpeed;
        this.enabled = true;
    }

    public bool UseStamina(int value)
    {
        if(stats.CurrentStamina<= value)
        {
            return true;
        }
        else
        {
            stats.CurrentStamina -= value;
            ConditionUI._stamina.value = stats.CurrentStamina;
            return false;
        }
    }
    // 프로퍼티
    public PlayerData Stats => stats;
    public Inventory Inventory => inventory;  // 인벤토리 반환
    public string PlayerNickName { get => nickName; set => nickName = value; }
}
