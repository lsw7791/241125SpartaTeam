using UnityEngine;
using UnityEngine.UI;

public class InGameUITest : UIBaseTest
{
    [SerializeField] private Button _btnMap;
    [SerializeField] private Button _btnStatus;
    [SerializeField] private Button _btnInventory;
    [SerializeField] private Button _btnOption;

    private MapUITest _mapUi;
    private StatusUITest _statusUi;
    private InventoryUITest _inventoryUi;
    private OptionUITest _optionUi;

    private void Start()
    {
        _btnMap.onClick.AddListener(() =>
        {
            OpenUI(ref _mapUi);
        });

        _btnStatus.onClick.AddListener(() =>
        {
            OpenUI(ref _statusUi);
        });

        _btnInventory.onClick.AddListener(() =>
        {
            OpenUI(ref _inventoryUi);
        });

        _btnOption.onClick.AddListener(() =>
        {
            OpenUI(ref _optionUi);
        });
    }

    private void OpenUI<T>(ref T inUI) where T : UIBaseTest
    {
        if (inUI != null && inUI.gameObject.activeSelf)
        {
            inUI.Close();
            return;
        }

        inUI = UIManagerTest.Instance.OpenUI<T>();
        inUI.gameObject.transform.SetParent(transform);

        inUI.rectTransform.anchoredPosition = Vector2.zero;
    }
}
