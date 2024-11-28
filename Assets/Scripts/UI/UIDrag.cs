using UnityEngine.EventSystems;
using UnityEngine;

public interface IDraggable
{
    void OnPointerDown(PointerEventData eventData);
    void OnDrag(PointerEventData eventData);
    void OnPointerUp(PointerEventData eventData);
}

public class UIDrag : MonoBehaviour, IDraggable, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
