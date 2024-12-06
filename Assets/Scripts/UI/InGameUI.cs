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
            GameManager.Instance.uIManager.ToggleUI<MapUI>();
        });

        _btnStatus.onClick.AddListener(() =>
        {
            GameManager.Instance.uIManager.ToggleUI<StatusUI>();
        });

        _btnInventory.onClick.AddListener(() =>
        {
            GameManager.Instance.uIManager.ToggleUI<InventoryUI>();
        });

        _btnOption.onClick.AddListener(() =>
        {
            GameManager.Instance.uIManager.ToggleUI<OptionUI>();
        });
    }
}
