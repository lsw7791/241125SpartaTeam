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
            TakeDamage(GameManager.Instance.Player.Stats.MineDamage);
        }
    }
    public void TakeDamage(int damage)
    {
        int value = damage - GameManager.Instance.DataManager.Creature.GetDefense(id);
        if (value > 0)
        {
        currentHealth -= value;
        if (currentHealth <= 0) Die();
        }
    }
    public void Die()
    {
        isDie = true;  // ���� ���� ó��
        minefull.ObjectSetActive(false);

        DropItems();  // ������ ��� �Լ� ȣ��
    }
    public void DropItems()
    {

        foreach (int itemId in GameManager.Instance.DataManager.Creature.GetDropItemIds(id))
        {
            var itemData = GameManager.Instance.DataManager.GetItemDataById(itemId);

            if (itemData != null)
            {
                // ������ ������ �ε�
                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

                    // ������ ������ ����
                    TestItem testItem = item.GetComponent<TestItem>();
                    if (testItem != null)
                    {
                        testItem.SetData(itemData);  // ������ �����Ϳ� ��������Ʈ ����
                    }
                }
            }

        }
    }
    public void ResetStatus()
    {
        currentHealth = GameManager.Instance.DataManager.Creature.GetHealth(id);  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
    }
}
