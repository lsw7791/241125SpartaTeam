using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; // ���Կ� ǥ�õ� ������ �̹���
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text count;  // ������ ���� �ؽ�Ʈ
    private InventoryItem item;  // ���Կ� �Ҵ�� ������

    [SerializeField] private GameObject _enhenceCountImage;
    [SerializeField] private TMP_Text _enhenceCount;

    // ���� Ŭ�� �� ������ ��� �޴� Ȱ��ȭ
    private void Start()
    {
        Button slotButton = GetComponent<Button>();

        slotButton.onClick.AddListener(() =>
        {
            GameObject itemUseMenu = UIManager.Instance.GetUI<InventoryUI>().itemUseMenu;
            itemUseMenu.SetActive(true);
            itemUseMenu.TryGetComponent<ItemUseUI>(out var outUseUI);
            outUseUI.Initialize(item);  // ������ ��� UI �ʱ�ȭ
        });
    }

    // ������ �ʱ�ȭ (�������� ���Կ� ǥ��)
    public void Initialize(InventoryItem inItem)
    {
        if (inItem == null)
        {
            ClearSlot();
            return;
        }
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int tierIndex = itemData.tier - 1;

        _backgroundImage.color = inItem.TierColoer(tierIndex);

        item = inItem;
        itemImage.sprite = inItem.ItemIcon;  // ������ ������ ����
        bool isSprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);

        if(isSprite)
        {
            itemImage.sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
            _enhenceCountImage.SetActive(true);
            _enhenceCount.text = $"{item.enhenceCount}";
        }
        else
        {
            itemImage.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
            _enhenceCountImage.SetActive(false);
        }

        itemImage.enabled = true;            // �̹��� ǥ��
        count.text = inItem.IsEquipped?"E":inItem.Quantity.ToString();// ���� ǥ��

        gameObject.SetActive(true);          // ���� Ȱ��ȭ
    }

    // �� ���� ó�� (���� ��Ȱ��ȭ)
    public void ClearSlot()
    {
        itemImage.enabled = false;   // �̹��� �����
        count.text = string.Empty;   // ���� �ؽ�Ʈ ����
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ
    }
}
