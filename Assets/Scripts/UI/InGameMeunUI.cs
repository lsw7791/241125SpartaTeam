using UnityEngine;
using UnityEngine.EventSystems; // EventTrigger 및 Pointer 이벤트 사용
using DG.Tweening; // 두트윈 사용

public class InGameMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject childObject; // 자식 오브젝트
    public float expandDuration = 0.3f; // 확산(확대) 애니메이션 지속 시간
    public float startScale = 0.5f; // 초기 크기
    public float endScale = 1.0f; // 최종 크기

    private RectTransform childRect; // 자식 오브젝트의 RectTransform

    void Start()
    {
        childRect = childObject.GetComponent<RectTransform>();
        childObject.SetActive(false); // 처음에는 자식 오브젝트 비활성화
    }

    // 마우스가 부모 오브젝트에 들어갔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        childObject.SetActive(true); // 자식 오브젝트 활성화
        childRect.localScale = Vector3.one * startScale; // 초기 크기 설정
        childRect.DOScale(endScale, expandDuration).SetEase(Ease.OutBack); // 확산 애니메이션
    }

    // 마우스가 부모 오브젝트에서 나갔을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        childRect.DOScale(startScale, expandDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            childObject.SetActive(false); // 축소 후 비활성화
        });
    }
}
