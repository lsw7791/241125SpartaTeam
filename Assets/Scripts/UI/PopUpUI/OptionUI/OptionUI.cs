using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    public Slider BGMSlider; // BGM 볼륨 조절 슬라이더
    public Slider SFXSlider; // SFX 볼륨 조절 슬라이더
    public RectTransform BGMSwitchImage;
    public RectTransform SFXSwitchImage;


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
    public void SetBGMMute()
    {
        Debug.Log("BGM");

        // 현재 Y축 회전 상태를 확인하여 180도 토글
        Vector3 currentRotation = BGMSwitchImage.localEulerAngles;
        BGMSwitchImage.localEulerAngles = new Vector3(
            currentRotation.x,
            (currentRotation.y == 0) ? 180 : 0, // 0과 180을 번갈아 설정
            currentRotation.z
        );

        // BGM 음소거 처리
        SoundManager.Instance.SetMuteBGM();
    }

    public void SetSFXMute()
    {
        // 현재 Y축 회전 상태를 확인하여 180도 토글
        Vector3 currentRotation = SFXSwitchImage.localEulerAngles;
        SFXSwitchImage.localEulerAngles = new Vector3(
            currentRotation.x,
            (currentRotation.y == 0) ? 180 : 0, // 0과 180을 번갈아 설정
            currentRotation.z
        );

        // SFX 음소거 처리
        SoundManager.Instance.SetMuteSFX();
    }
    public void OnclickedExitBtn()
    {
        // 옵션 UI 닫기 및 씬 이동 처리
        UIManager.Instance.CloseUI<OptionUI>();
    }

    public void OnclickedMainMenuBtn()
    {
        UIManager.Instance.CloseUI<OptionUI>();
        GameManager.Instance.DataManager.SaveData();
        GameManager.Instance.SceneNum = 25;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    public void OptionUIOn()
    {
        // 옵션 UI 토글
        UIManager.Instance.ToggleUI<OptionUI>();
    }
}