using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class MineHealthBarUI : MonoBehaviour
{
    Mine mine;
    [Header("Health Settings")]
    public float maxHealth; // 최대 체력
    [SerializeField] private float currentHealth;  // 현재 체력

    [Header("Health Bar")]
    public Image healthBarForeground; // 체력바 (초록색 부분)
    public Image healthBarBackground; // 체력바 배경 (옵션)
    private void Awake()
    {
        mine = GetComponent<Mine>();     
        maxHealth = GameManager.Instance.DataManager.Creature.GetHealth(mine.id);
    }
    private void Start()
    {
        currentHealth = mine.currentHealth;
        UpdateHealthBar();
    }
    void Update()
    {
        if (!RendererIsVisible())
        {
            healthBarForeground.gameObject.SetActive(false); // 화면에 보이지 않으면 비활성화
        }
        else
        {
            healthBarForeground.gameObject.SetActive(true);
        }
    }

    // 화면 안에 있는지 확인하는 함수
    private bool RendererIsVisible()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        return renderer.isVisible;
    }
    // 체력바 업데이트
    public void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            // 현재 체력 비율 (0.0 ~ 1.0)
            currentHealth = mine.currentHealth;
            maxHealth = GameManager.Instance.DataManager.Creature.GetHealth(mine.id);
            healthBarForeground.fillAmount = currentHealth / maxHealth;
            Debug.Log($"currentHealth = {currentHealth}");
            Debug.Log($"maxHealth = {maxHealth}");
            Debug.Log($"healthBarForeground.fillAmount = {healthBarForeground.fillAmount}");


        }
    }
}
