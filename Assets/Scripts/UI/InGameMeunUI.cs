using UnityEngine;
using UnityEngine.EventSystems; // EventTrigger �� Pointer �̺�Ʈ ���
using DG.Tweening; // ��Ʈ�� ���

public class InGameMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject childObject; // �ڽ� ������Ʈ
    public float expandDuration = 0.3f; // Ȯ��(Ȯ��) �ִϸ��̼� ���� �ð�
    public float startScale = 0.5f; // �ʱ� ũ��
    public float endScale = 1.0f; // ���� ũ��

    private RectTransform childRect; // �ڽ� ������Ʈ�� RectTransform

    // UI ������Ʈ��
    public GameObject mapUI;         // �� UI
    public GameObject inventoryUI;   // �κ��丮 UI
    public GameObject optionUI;     // �ɼ� UI
    public GameObject statusUI;      // �����ͽ� UI

    void Start()
    {
        inventoryUI = UIManager.Instance.inventoryUI;
        statusUI = UIManager.Instance.statusUI;
        mapUI = UIManager.Instance.mapUI;
        optionUI = UIManager.Instance.optionUI;
        childRect = childObject.GetComponent<RectTransform>();
        childObject.SetActive(false); // ó������ �ڽ� ������Ʈ ��Ȱ��ȭ
    }

    // ���콺�� �θ� ������Ʈ�� ���� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        childObject.SetActive(true); // �ڽ� ������Ʈ Ȱ��ȭ
        childRect.localScale = Vector3.one * startScale; // �ʱ� ũ�� ����
        childRect.DOScale(endScale, expandDuration).SetEase(Ease.OutBack); // Ȯ�� �ִϸ��̼�
    }

    // ���콺�� �θ� ������Ʈ���� ������ ��
    public void OnPointerExit(PointerEventData eventData)
    {
        childRect.DOScale(startScale, expandDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            childObject.SetActive(false); // ��� �� ��Ȱ��ȭ
        });
    }

    // �� UI ���
    public void ToggleMapUI()
    {
        mapUI.SetActive(!mapUI.activeSelf); // Ȱ��ȭ/��Ȱ��ȭ ���
    }

    // �κ��丮 UI ���
    public void ToggleInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf); // Ȱ��ȭ/��Ȱ��ȭ ���
    }

    // �ɼ� UI ���
    public void ToggleOptionUI()
    {
        optionUI.SetActive(!optionUI.activeSelf); // Ȱ��ȭ/��Ȱ��ȭ ���
    }

    // �����ͽ� UI ���
    public void ToggleStatusUI()
    {
        statusUI.SetActive(!statusUI.activeSelf); // Ȱ��ȭ/��Ȱ��ȭ ���
    }
}
