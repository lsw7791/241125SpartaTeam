using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    [SerializeField] private Slider _hp;       // 체력 슬라이더
    [SerializeField] private Slider _stamina; // 스태미너 슬라이더

    private void Start()
    {
        InitializeSliders();
    }

    private void Update()
    {
        UpdateSliders();
    }

    // 슬라이더를 초기화하는 메서드
    private void InitializeSliders()
    {
        var playerStats = GameManager.Instance.Player.stats;
        if (playerStats == null) return;

        _hp.maxValue = playerStats.MaxHP;
        _hp.value = playerStats.CurrentHP;

        _stamina.maxValue = playerStats.MaxStamina;
        _stamina.value = playerStats.CurrentStamina;
    }

    // 슬라이더를 지속적으로 업데이트하는 메서드
    private void UpdateSliders()
    {
        var playerStats = GameManager.Instance.Player.stats;
        if (playerStats == null) return;

        _hp.value = playerStats.CurrentHP;
        _stamina.value = playerStats.CurrentStamina;
    }
}
