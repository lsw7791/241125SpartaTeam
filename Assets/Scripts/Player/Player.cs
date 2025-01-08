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

    public PlayerData stats;  // 플레이어의 스탯
    public Inventory inventory;  // 플레이어의 인벤토리
    public PlayerInput PlayerInput;
    public PlayerAnimationController _playerAnimationController;
    public PlayerWeapon _playerWeapon;
    public GameObject Weapon;
    public ConditionUI ConditionUI;
    public StatusUI StatusUI;
    private float staminaRechargeTimer = 0f;
    public PlayerState playerState = PlayerState.Idle;
    public PlayerRoll playerRoll;
    // QuickSlots 프로퍼티

    public TMP_Text playerNameText;   // 이름을 표시할 TextMeshPro

    public bool isDamage = false;

    public QuickSlot QuickSlots { get; private set; }  // QuickSlot 객체로 변경
    public delegate void PlayerDataSavedHandler();
    //public event PlayerDataSavedHandler OnPlayerDataSaved;
    public Rigidbody2D _playerRB;

    public Player()
    {
        inventory = new Inventory();
        //stats = GameManager.Instance.DataManager.nowPlayer;
        QuickSlots = new QuickSlot();  // QuickSlot 객체 초기화

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

    // 플레이어 죽음 처리
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
        _playerAnimationController.TriggerDeathAnimation(); // 죽음 애니메이션 실행
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

    // 방어력 복구
    public void ResetDefense()
    {
        stats.CurrentDef = stats.Def; // 방어력을 원래대로 복구
    }
    public void StopRolling()
    {
        playerRoll.isRolling = false;
        playerRoll.traveledDistance = 0f; // 이동 거리 초기화
        _playerRB.velocity = Vector2.zero; // Rigidbody 속도 초기화
        GameManager.Instance.Player.ResetDefense(); // 방어력 초기화
    }
    public void PlayerStateUIOpen()
    {
        playerState = PlayerState.UIOpen;
    }
    public void PlayerStateIdle()
    {
        playerState = PlayerState.Idle;
    }
    // 프로퍼티
    public PlayerData Stats => stats;
    public Inventory Inventory => inventory;  // 인벤토리 반환
}
