using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBarUI : MonoBehaviour
{
    Monster monster;
    [Header("Health Settings")]
    public float maxHealth; // 최대 체력
    private float currentHealth;  // 현재 체력

    [Header("Health Bar")]
    public Image healthBarForeground; // 체력바 (초록색 부분)
    public Image healthBarBackground; // 체력바 배경 (옵션)

    private void Start()
    {
        monster = GetComponent<Monster>();
        maxHealth = GameManager.Instance.DataManager.Creature.GetHealth(monster.id);
        // 초기 체력을 최대 체력으로 설정
        currentHealth = monster.currentHealth;
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
            currentHealth = monster.currentHealth;
            float targetFill = currentHealth / maxHealth;
            Debug.Log($"현재 체력 : {currentHealth}, 최대 체력 : {maxHealth}");
            // 체력바의 fillAmount를 부드럽게 변화
            //healthBarForeground.fillAmount = Mathf.Lerp(healthBarForeground.fillAmount, targetFill, Time.deltaTime );
            healthBarForeground.fillAmount = targetFill;
        }
    }
}
