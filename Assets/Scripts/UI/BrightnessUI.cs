using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessUI : MonoBehaviour
{
    private Image BPanel; // Panel�� Image ������Ʈ

    private void Awake()
    {
        BPanel = GetComponentInChildren<Image>();
    }

    public void SetBrightness(float value)
    {
        Color color = BPanel.color; // ���� �÷� �� ��������
        color.a = value; // ���� �� ����
        BPanel.color = color; // ������ �÷��� �ٽ� ����
    }
    public float GetBrightnessA() => BPanel.color.a;
}
