using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BtnColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private Color _originalColor;
    private TMP_Text _text;

    void Start()
    {
        // Button 컴포넌트 가져오기
        _button = GetComponent<Button>();
        // 버튼의 원래 색상 저장하기
        _text = GetComponent<TMP_Text>();  
        _originalColor = _text.color;
    }

    // 마우스가 버튼 위에 올라갔을 때 색 변경
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = new Color(208f / 255f, 177f / 255f, 245f / 255f);  // RGB(208, 177, 245)로 색상 변경
    }

    // 마우스가 버튼을 떠났을 때 원래 색으로 돌아가기
    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _originalColor;  // 원래 색상으로 복원
    }
}
