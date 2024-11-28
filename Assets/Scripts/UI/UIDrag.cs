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

    // 드래그 시작 시 호출
    public void OnPointerDown(PointerEventData eventData)
    {
        // 드래그 시작 시의 UI 위치와, 마우스 위치의 차이를 계산하여 offset을 저장
        offset = (Vector2)rectTransform.position - eventData.position;
    }

    // 드래그 중 호출
    public void OnDrag(PointerEventData eventData)
    {
        // 마우스의 위치에 offset을 더해서 UI의 위치를 계속해서 이동
        rectTransform.position = eventData.position + offset;
    }

    // 드래그 종료 시 호출
    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
