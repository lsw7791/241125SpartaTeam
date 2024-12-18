using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBarUI : MonoBehaviour
{
    Monster monster;
    [Header("Health Settings")]
    public float maxHealth; // �ִ� ü��
    private float currentHealth;  // ���� ü��

    [Header("Health Bar")]
    public Image healthBarForeground; // ü�¹� (�ʷϻ� �κ�)
    public Image healthBarBackground; // ü�¹� ��� (�ɼ�)

    private void Start()
    {
        monster = GetComponent<Monster>();
        maxHealth = GameManager.Instance.DataManager.Creature.GetHealth(monster.id);
        // �ʱ� ü���� �ִ� ü������ ����
        currentHealth = monster.currentHealth;
        UpdateHealthBar();
    }
    void Update()
    {
        if (!RendererIsVisible())
        {
            healthBarForeground.gameObject.SetActive(false); // ȭ�鿡 ������ ������ ��Ȱ��ȭ
        }
        else
        {
            healthBarForeground.gameObject.SetActive(true);
        }
    }

    // ȭ�� �ȿ� �ִ��� Ȯ���ϴ� �Լ�
    private bool RendererIsVisible()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        return renderer.isVisible;
    }
    // ü�¹� ������Ʈ
    private void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            float targetFill = currentHealth / maxHealth;
            healthBarForeground.fillAmount = Mathf.Lerp(healthBarForeground.fillAmount, targetFill, Time.deltaTime * 10);
        }
    }
}
