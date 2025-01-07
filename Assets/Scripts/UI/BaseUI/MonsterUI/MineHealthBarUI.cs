using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class MineHealthBarUI : MonoBehaviour
{
    Mine mine;
    [Header("Health Bar")]
    public Image healthBarForeground; // ü�¹� (�ʷϻ� �κ�)
    public Image healthBarBackground; // ü�¹� ��� (�ɼ�)

    private void Start()
    {
        mine = GetComponent<Mine>();
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
    public void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            // ���� ü�� ���� (0.0 ~ 1.0)
            
            healthBarForeground.fillAmount = mine.currentHealth / GameManager.Instance.DataManager.Creature.GetHealth(mine.id);
            Debug.Log($"healthBarForeground.fillAmount ={healthBarForeground.fillAmount}");
            Debug.Log($"Creature.GetHealth(mine.id) = {GameManager.Instance.DataManager.Creature.GetHealth(mine.id)}");

            Debug.Log($"mine.currentHealth = {mine.currentHealth}");
        }
    }
}
