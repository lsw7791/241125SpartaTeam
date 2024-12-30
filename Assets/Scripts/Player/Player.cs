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
    public PlayerCamera _playerCamera;
    public Equipment equipment;
    public GameObject Weapon;
    public ConditionUI ConditionUI;
    public StatusUI StatusUI;

    // QuickSlots ������Ƽ
    public QuickSlot QuickSlots { get; private set; }  // QuickSlot ��ü�� ����

    public delegate void PlayerDataSavedHandler();
    public event PlayerDataSavedHandler OnPlayerDataSaved;

    public Player()
    {
        inventory = new Inventory();
        stats = new PlayerData();
        QuickSlots = new QuickSlot();  // QuickSlot ��ü �ʱ�ȭ
    }

    private void Awake()
    {
        stats.Initialize();
        PlayerInput = GetComponent<PlayerInput>();
        equipment = GetComponent<Equipment>();
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
        GameManager.Instance.SceneNum = 23;
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


    // ������Ƽ
    public PlayerData Stats => stats;
    public Inventory Inventory => inventory;  // �κ��丮 ��ȯ
    public string PlayerNickName { get => nickName; set => nickName = value; }
}
