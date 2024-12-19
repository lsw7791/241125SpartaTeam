using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Slider _hp;       // ü�� �����̴�
    [SerializeField] private Slider _stamina; // ���¹̳� �����̴�

    private void Start()
    {
        InitializeSliders();
    }

    private void Update()
    {
        UpdateSliders();
    }

    // �����̴��� �ʱ�ȭ�ϴ� �޼���
    private void InitializeSliders()
    {
        var playerStats = GameManager.Instance.Player.stats;
        if (playerStats == null) return;

        _hp.maxValue = playerStats.MaxHP;
        _hp.value = playerStats.CurrentHP;

        _stamina.maxValue = playerStats.MaxStamina;
        _stamina.value = playerStats.CurrentStamina;
    }

    // �����̴��� ���������� ������Ʈ�ϴ� �޼���
    private void UpdateSliders()
    {
        var playerStats = GameManager.Instance.Player.stats;
        if (playerStats == null) return;

        _hp.value = playerStats.CurrentHP;
        _stamina.value = playerStats.CurrentStamina;
    }
}
