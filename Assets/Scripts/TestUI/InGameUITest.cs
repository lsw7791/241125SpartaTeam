using UnityEngine;
using UnityEngine.UI;

public class InGameUITest : MonoBehaviour
{
    [SerializeField] private Button _btnMap;
    [SerializeField] private Button _btnStatus;
    [SerializeField] private Button _btnInventory;
    [SerializeField] private Button _btnOption;

    private void Start()
    {
        _btnMap.onClick.AddListener(() =>
        {
            UIManagerTest.Instance.OpenUI<MapUITest>();
        });

        _btnStatus.onClick.AddListener(() =>
        {
            UIManagerTest.Instance.OpenUI<StatusUITest>();
        });

        _btnInventory.onClick.AddListener(() =>
        {
            UIManagerTest.Instance.OpenUI<InventoryUITest>();
        });

        _btnOption.onClick.AddListener(() =>
        {
            UIManagerTest.Instance.OpenUI<OptionUITest>();
        });
    }
}
