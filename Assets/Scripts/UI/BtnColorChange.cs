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
        // Button ������Ʈ ��������
        _button = GetComponent<Button>();
        // ��ư�� ���� ���� �����ϱ�
        _text = GetComponent<TMP_Text>();  
        _originalColor = _text.color;
    }

    // ���콺�� ��ư ���� �ö��� �� �� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = new Color(208f / 255f, 177f / 255f, 245f / 255f);  // RGB(208, 177, 245)�� ���� ����
    }

    // ���콺�� ��ư�� ������ �� ���� ������ ���ư���
    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _originalColor;  // ���� �������� ����
    }
}
