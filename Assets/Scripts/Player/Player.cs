using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public enum PlayerState
    { 
    Idle,
    Attack,
    Die,
    UIOpen,
    GetDamaged,
    MoveMap
    }

    public PlayerData stats;  // �÷��̾��� ����
    public Inventory inventory;  // �÷��̾��� �κ��丮
    public PlayerInput PlayerInput;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public GameObject Weapon;
    public ConditionUI ConditionUI;
    public StatusUI StatusUI;
    private float staminaRechargeTimer = 0f;
    public PlayerState playerState = PlayerState.Idle;
    public PlayerRoll playerRoll;
    // QuickSlots ������Ƽ

    public TMP_Text playerNameText;   // �̸��� ǥ���� TextMeshPro

    public bool isDamage = false;

    public QuickSlot QuickSlots { get; private set; }  // QuickSlot ��ü�� ����
    public delegate void PlayerDataSavedHandler();
    //public event PlayerDataSavedHandler OnPlayerDataSaved;
    public Rigidbody2D _playerRB;

    public Player()
    {
        inventory = new Inventory();
        //stats = GameManager.Instance.DataManager.nowPlayer;
        QuickSlots = new QuickSlot();  // QuickSlot ��ü �ʱ�ȭ

    }

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        _playerWeapon = Weapon.GetComponent<PlayerWeapon>();
        _playerRB = GetComponent<Rigidbody2D>();
        playerRoll = GetComponent<PlayerRoll>();
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
        if(playerState == PlayerState.Die)
        {
            return;
        }

        if (!isDamage)
        {
            StartCoroutine(DamageDelay(damage));
        }
    }

    IEnumerator DamageDelay(int inDamage)
    {
        isDamage = true;

        WaitForSeconds delayTime = new WaitForSeconds(2f);

        SoundManager.Instance.PlayPunchSFX();
        int value = stats.CurrentDef;
        value -= inDamage;
        if (value < 0)
        {
            stats.CurrentHP += value;
            ConditionUI.UpdateSliders();
            if (stats.CurrentHP <= 0)
            {
                Die();
            }
        }

        yield return delayTime;

        isDamage = false;
    }

    // �÷��̾� ���� ó��
    public void Die()
    {
        playerState = PlayerState.Die;
        TriggerDeath();

        GameManager.Instance.SceneNum = 2;
        UIManager.Instance.fadeManager.LoadSceneWithFade(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));

    }
    public void TriggerDeath()
    {
        stats.isDie = true;
        _playerAnimationController.TriggerDeathAnimation(); // ���� �ִϸ��̼� ����
        this.enabled = false;

    }
    public void Revive()
    {
        playerState = PlayerState.Idle;
        stats.isDie = false;
        isDamage = false;
        stats.CurrentHP = 20;
        ConditionUI.UpdateSliders();
        this.enabled = true;
    }

    public bool UseStamina(int value)
    {
        if (stats.CurrentStamina <= value)
        {
            return false;
        }
        else
        {
            stats.CurrentStamina -= value;
            ConditionUI._stamina.value = stats.CurrentStamina;
            return true;
        }
    }

    public void IncreaseDefense(int value)
    {
        stats.CurrentDef *= value;
    }

    // ���� ����
    public void ResetDefense()
    {
        stats.CurrentDef = stats.Def; // ������ ������� ����
    }
    public void StopRolling()
    {
        playerRoll.isRolling = false;
        playerRoll.traveledDistance = 0f; // �̵� �Ÿ� �ʱ�ȭ
        _playerRB.velocity = Vector2.zero; // Rigidbody �ӵ� �ʱ�ȭ
        GameManager.Instance.Player.ResetDefense(); // ���� �ʱ�ȭ
    }
    public void PlayerStateUIOpen()
    {
        playerState = PlayerState.UIOpen;
    }
    public void PlayerStateIdle()
    {
        playerState = PlayerState.Idle;
    }
    // ������Ƽ
    public PlayerData Stats => stats;
    public Inventory Inventory => inventory;  // �κ��丮 ��ȯ
}
