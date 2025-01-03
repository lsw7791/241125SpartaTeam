using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public string nickName;

    public PlayerData stats;  // �÷��̾��� ����
    public Inventory inventory;  // �÷��̾��� �κ��丮
    public PlayerInput PlayerInput;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public Equipment equipment;
    public GameObject Weapon;
    public ConditionUI ConditionUI;
    public StatusUI StatusUI;
    private float staminaRechargeTimer = 0f;
    // QuickSlots ������Ƽ
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot ��ü�� ����

    public delegate void PlayerDataSavedHandler();
    //public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        inventory = new Inventory();
        stats = GameManager.Instance.DataManager.nowPlayer;
        QuickSlots = new QuickSlot();  // QuickSlot ��ü �ʱ�ȭ

    }

    private void Awake()
    {
        //stats.Initialize();
        PlayerInput = GetComponent<PlayerInput>();
        equipment = GetComponent<Equipment>();
    }

    private void FixedUpdate()
    {
        // FixedUpdate�� �ð� ������ Time.fixedDeltaTime���� �����Ǿ� ����
        staminaRechargeTimer += Time.fixedDeltaTime;

        // 1�ʸ��� ����
        if (staminaRechargeTimer >= 1f)
        {
            if (stats.MaxStamina > stats.CurrentStamina)
            {
                stats.CurrentStamina += 5;
                ConditionUI._stamina.value = stats.CurrentStamina; // UI ������Ʈ
            }
            staminaRechargeTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }
    // �κ��丮 ����: ��������Ʈ ����
    public void AddItemToInventory(int itemID, int quantity, string spritePath)
    { // ������ �κ��丮�� �߰�
        inventory.AddItem(itemID, 1);  // ��������Ʈ ����
    }

    // ������ ó��
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

    // �÷��̾� ���� ó��
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
        GameManager.Instance.Player._playerAnimationController.TriggerDeathAnimation(); // ���� �ִϸ��̼� ����
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
    // ������Ƽ
    public PlayerData Stats => stats;
    public Inventory Inventory => inventory;  // �κ��丮 ��ȯ
    public string PlayerNickName { get => nickName; set => nickName = value; }
}
