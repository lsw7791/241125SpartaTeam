using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    public Slider BGMSlider; // BGM ���� ���� �����̴�
    public Slider SFXSlider; // SFX ���� ���� �����̴�

    private void Start()
    {
        // �ʱ� �����̴� �� ����
        BGMSlider.value = SoundManager.Instance.GetBGMVolume();
        SFXSlider.value = SoundManager.Instance.GetSFXVolume();

        // �����̴� �̺�Ʈ ���
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
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

    public void OnclickedExitBtn()
    {
        // �ɼ� UI �ݱ� �� �� �̵� ó��
        UIManager.Instance.ToggleUI<OptionUI>();
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    public void OptionUIOn()
    {
        // �ɼ� UI ���
        UIManager.Instance.ToggleUI<OptionUI>();
    }
}