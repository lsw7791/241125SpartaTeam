using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    public Slider BGMSlider; // BGM 볼륨 조절 슬라이더
    public Slider SFXSlider; // SFX 볼륨 조절 슬라이더

    private void Start()
    {
        // 초기 슬라이더 값 설정
        BGMSlider.value = SoundManager.Instance.GetBGMVolume();
        SFXSlider.value = SoundManager.Instance.GetSFXVolume();

        // 슬라이더 이벤트 등록
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetBGMVolume(float volume)
    {
        // SoundManager를 통해 BGM 볼륨 설정
        SoundManager.Instance.SetBGMVolume(volume);
        PlayerPrefs.SetFloat("BGMVolume", volume); // 설정 저장
    }

    public void SetSFXVolume(float volume)
    {
        // SoundManager를 통해 SFX 볼륨 설정
        SoundManager.Instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume); // 설정 저장
    }

    public void OnclickedExitBtn()
    {
        // 옵션 UI 닫기 및 씬 이동 처리
        UIManager.Instance.ToggleUI<OptionUI>();
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    public void OptionUIOn()
    {
        // 옵션 UI 토글
        UIManager.Instance.ToggleUI<OptionUI>();
    }
}