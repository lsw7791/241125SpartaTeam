using UnityEngine;
using UnityEngine.EventSystems;

public interface IDraggable
{
    void OnPointerDown(PointerEventData eventData);
    void OnDrag(PointerEventData eventData);
    void OnPointerUp(PointerEventData eventData);
}

public abstract class UIBase : MonoBehaviour, IDraggable, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform rectTransform;
    private Vector2 offset;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        OpenProcedure();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        CloseProcedure();
    }

    protected virtual void OpenProcedure()
    { // ���� �� �߰� �������

    }

    protected virtual void CloseProcedure()
    { // ��Ȱ��ȭ �� �߰� ���� ����

    }

    // �巡�� ���� �� ȣ��
    public void OnPointerDown(PointerEventData eventData)
    {
        // �巡�� ���� ���� UI ��ġ��, ���콺 ��ġ�� ���̸� ����Ͽ� offset�� ����
        offset = (Vector2)rectTransform.position - eventData.position;
    }

    // �巡�� �� ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        // ���콺�� ��ġ�� offset�� ���ؼ� UI�� ��ġ�� ����ؼ� �̵�
        rectTransform.position = eventData.position + offset;
    }

    // �巡�� ���� �� ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
