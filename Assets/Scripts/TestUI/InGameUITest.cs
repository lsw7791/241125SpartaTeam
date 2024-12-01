using Constants;
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
            if (_mapUi != null)
            {
                _mapUi.Close();
                return;
            }

            if (_mapUi == null)
                _mapUi = UIManagerTest.Instance.GetUI<MapUITest>();
            if (_mapUi != null)
                _mapUi.Open();
        });

        _btnStatus.onClick.AddListener(() =>
        {
            if (_statusUi != null)
            {
                _statusUi.Close();
                return;
            }

            if (_statusUi == null)
                _statusUi = UIManagerTest.Instance.GetUI<StatusUITest>();
            if (_statusUi != null)
                _statusUi.Open();
        });

        _btnInventory.onClick.AddListener(() =>
        {
            if (_inventoryUi != null)
            {
                _inventoryUi.Close();
                return;
            }

            if (_inventoryUi == null)
                _inventoryUi = UIManagerTest.Instance.GetUI<InventoryUITest>();
            if (_inventoryUi != null)
                _inventoryUi.Open();
        });

        _btnOption.onClick.AddListener(() =>
        {
            if (_optionUi != null)
            {
                _optionUi.Close();
                return;
            }

            if (_optionUi == null)
                _optionUi = UIManagerTest.Instance.GetUI<OptionUITest>();
            if (_optionUi != null)
                _optionUi.Open();
        });
    }
}
