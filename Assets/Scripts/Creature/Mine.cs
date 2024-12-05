using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICreature
{
    
    GameManager gameManager;   // ���� �Ŵ���
    MineFull minefull;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isDie;
    [SerializeField] public int id;

    private void Awake()
    {
        gameManager = GameManager.Instance;  // ���� �Ŵ��� �ν��Ͻ� ��������
        minefull = GetComponentInChildren<MineFull>();
    }
    private void Start()
    {
        // 30�ʸ��� ObjectSetActive ȣ��
        StartCoroutine(ActivateObjectEvery30Seconds());
    }
    private IEnumerator ActivateObjectEvery30Seconds()
    {
        while (true)
        {
            // 30�� ��ٸ� ��
            yield return new WaitForSeconds(30f);

            // SetActive ȣ��
            if (isDie == true)
            {
                minefull.ObjectSetActive(true);
                isDie = false;
                ResetStatus();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Weapon"))
        {
            // Weapon ��ũ��Ʈ���� id ���� ��������
            PlayerWeapon playerWeapon = collision.collider.GetComponent<PlayerWeapon>();

            // �������� �޴� �޼��� ȣ��
            TakeDamage(Player.Instance.Stats.Damage);
        }
    }
    public void TakeDamage(int damage)
    {       
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }
    public void Die()
    {
        isDie = true;  // ���� ���� ó��
        minefull.ObjectSetActive(false);

        DropItems();  // ������ ��� �Լ� ȣ��
    }
    public void DropItems()
    {

        foreach (int itemId in DataManager.Instance.creature.GetDropItemIds(id))
        {
            var itemData = DataManager.Instance.GetItemDataById(itemId);

            if (itemData != null)
            {
                // ������ ������ �ε�
                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabsPath);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

                    // ������ ��������Ʈ �ε�
                    Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);
                    // ������ ������ ����
                    TestItem testItem = item.GetComponent<TestItem>();
                    if (testItem != null)
                    {
                        testItem.SetData(itemData, itemSprite);  // ������ �����Ϳ� ��������Ʈ ����
                    }
                }
            }

        }
    }
    public void ResetStatus()
    {
        currentHealth = DataManager.Instance.creature.GetHealth(id);  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
    }
}
