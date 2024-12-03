using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Button _btnMap;
    [SerializeField] private Button _btnStatus;
    [SerializeField] private Button _btnInventory;
    [SerializeField] private Button _btnOption;

    private void Start()
    {
        _btnMap.onClick.AddListener(() =>
        {
            UIManager.Instance.ToggleUI<MapUI>();
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
