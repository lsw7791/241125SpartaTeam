using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessUI : MonoBehaviour
{
    private Image BPanel; // Panel의 Image 컴포넌트

    private void Awake()
    {
        BPanel = GetComponentInChildren<Image>();
    }

    public void SetBrightness(float value)
    {
        Color color = BPanel.color; // 기존 컬러 값 가져오기
        color.a = value; // 알파 값 수정
        BPanel.color = color; // 수정된 컬러를 다시 적용
    }
    public float GetBrightnessA() => BPanel.color.a;
}
