using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    public Slider BGMSlider; // BGM ���� ���� �����̴�
    public Slider SFXSlider; // SFX ���� ���� �����̴�
    public Slider BrightSlider; // ���� ���� �����̴�

    public RectTransform BGMSwitchImage;
    public RectTransform SFXSwitchImage;


    private void Start()
    {
        // �ʱ� �����̴� �� ����
        BGMSlider.value = SoundManager.Instance.GetBGMVolume();
        SFXSlider.value = SoundManager.Instance.GetSFXVolume();
        BrightSlider.value = UIManager.Instance.brightnessUI.GetBrightnessA();

        // �����̴� �̺�Ʈ ���
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        BrightSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetBGMVolume(float volume)
    {
        // SoundManager�� ���� BGM ���� ����
        SoundManager.Instance.SetBGMVolume(volume);
        PlayerPrefs.SetFloat("BGMVolume", volume); // ���� ����
    }

    public void SetSFXVolume(float volume)
    {
        // SoundManager�� ���� SFX ���� ����
        SoundManager.Instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume); // ���� ����
    }
    public void SetBrightness(float value)
    {
        UIManager.Instance.brightnessUI.SetBrightness(value);
        PlayerPrefs.SetFloat("Brightness", value); // ���� ����
    }
    public void SetBGMMute()
    {
        Debug.Log("BGM");

        // ���� Y�� ȸ�� ���¸� Ȯ���Ͽ� 180�� ���
        Vector3 currentRotation = BGMSwitchImage.localEulerAngles;
        BGMSwitchImage.localEulerAngles = new Vector3(
            currentRotation.x,
            (currentRotation.y == 0) ? 180 : 0, // 0�� 180�� ������ ����
            currentRotation.z
        );

        // BGM ���Ұ� ó��
        SoundManager.Instance.SetMuteBGM();
    }

    public void SetSFXMute()
    {
        // ���� Y�� ȸ�� ���¸� Ȯ���Ͽ� 180�� ���
        Vector3 currentRotation = SFXSwitchImage.localEulerAngles;
        SFXSwitchImage.localEulerAngles = new Vector3(
            currentRotation.x,
            (currentRotation.y == 0) ? 180 : 0, // 0�� 180�� ������ ����
            currentRotation.z
        );

        // SFX ���Ұ� ó��
        SoundManager.Instance.SetMuteSFX();
    }
    public void OnclickedExitBtn()
    {
        // �ɼ� UI �ݱ� �� �� �̵� ó��
        UIManager.Instance.CloseUI<OptionUI>();
    }
    public void OptionUIOn()
    {
        // �ɼ� UI ���
        UIManager.Instance.ToggleUI<OptionUI>();
    }
}