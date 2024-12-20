using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Slider _hp;       // ü�� �����̴�
    [SerializeField] private Slider _stamina; // ���¹̳� �����̴�
    Player player;
    private void Start()
    {
        GameManager.Instance.Player.ConditionUI = this;
        InitializeSliders();
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
    public void UpdateSliders()
    {
        var playerStats = GameManager.Instance.Player.stats;
        if (playerStats == null) return;

        _hp.value = playerStats.CurrentHP;
        _stamina.value = playerStats.CurrentStamina;
    }
}
