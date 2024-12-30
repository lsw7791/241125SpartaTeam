using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Button _btnCheat;
    [SerializeField] private Button _btnStatus;
    [SerializeField] private Button _btnInventory;
    [SerializeField] private Button _btnOption;

    private void Start()
    {
        _btnCheat.onClick.AddListener(() =>
        {
            UIManager.Instance.ToggleUI<CheatUI>();
            // 치트 활성화
            var playerStats = GameManager.Instance.Player.Stats;
            playerStats.MaxHP = 999;
            playerStats.CurrentHP = 999;
            playerStats.MaxStamina = 999;
            playerStats.CurrentStamina = 999;
            playerStats.Damage = 999;
            playerStats.Def = 999;
            playerStats.MineDamage = 999;
            playerStats.PhysicalDamage = 999;
            playerStats.MagicalDamage = 999;
            StatusUI statusUI = UIManager.Instance.GetUI<StatusUI>();
            statusUI.Refresh();
            UIManager.Instance.ToggleUI<StatusUI>();
        });

        _btnStatus.onClick.AddListener(() =>
        {
            UIManager.Instance.ToggleUI<StatusUI>();
        });

        _btnInventory.onClick.AddListener(() =>
        {
            UIManager.Instance.ToggleUI<InventoryUI>();
        });

        _btnOption.onClick.AddListener(() =>
        {
            UIManager.Instance.ToggleUI<OptionUI>();
        });
    }
}
