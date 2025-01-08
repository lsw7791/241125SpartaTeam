﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private GameObject _bgmObj;
    private GameObject _sfxObj;

    public AudioSource _bgmSource;
    public AudioSource _sfxSource;

    private float lastSFXTime = 0f;
    public float minSFXInterval = 0.2f; // 최소 간격 (0.2초)

    [Header("BGM")]
    [SerializeField] private AudioClip AsiaTravelBGM;
    [SerializeField] private AudioClip MoodtimeflowBGM;
    [SerializeField] private AudioClip MysticalBGM;
    [SerializeField] private AudioClip RelaxingPianoBGM;
    [SerializeField] private AudioClip CalmPianoBGM;
    [SerializeField] private AudioClip ShootingStarBGM;


    [Header("SFX")]
    [SerializeField] private AudioClip ClearSFX;
    [SerializeField] private AudioClip FireBallSFX;
    [SerializeField] private AudioClip ItemPickUpSFX;
    [SerializeField] private AudioClip PunchSFX;
    [SerializeField] private AudioClip SwordSFX;
    [SerializeField] private AudioClip WinningSFX;
    [SerializeField] private AudioClip ArrowSFX;
    [SerializeField] private AudioClip DefenseSFX;
    [SerializeField] private AudioClip RollSFX;
    [SerializeField] private AudioClip BookSFX;
    [SerializeField] private AudioClip Button1SFX;
    [SerializeField] private AudioClip Button2SFX;
    [SerializeField] private AudioClip SuccessSFX; 
    [SerializeField] private AudioClip FailSFX;


    protected override void Awake()
    {
        base.Awake();
        SetAudioSource();
        SetAudioClip();

    }
    private void Start()
    {
        _bgmSource.volume = 0.3f;
        _sfxSource.volume = 0.15f;
        //PlayBGM(bgmClip); // BGM 시작 시 사용할 수 있도록 설정 (옵션)
    }

    private void SetAudioSource()
    {
        // AudioManager에서 사용할 AudioSource 객체 생성 @BGM
        _bgmObj = new GameObject("@BGM");
        _bgmObj.transform.parent = transform;
        _bgmSource = _bgmObj.AddComponent<AudioSource>();

        // AudioManager에서 사용할 AudioSource 객체 생성 @SFX
        _sfxObj = new GameObject("@SFX");
        _sfxObj.transform.parent = transform;
        _sfxSource = _sfxObj.AddComponent<AudioSource>();
    }

    private void SetAudioClip()
    {
        // Resources 폴더에서 AudioClip을 불러옵니다.//BGM
        MoodtimeflowBGM = Resources.Load<AudioClip>("Sounds/BGM/MoodtimeflowBGM");
        AsiaTravelBGM = Resources.Load<AudioClip>("Sounds/BGM/AsiaTravelBGM");
        MysticalBGM = Resources.Load<AudioClip>("Sounds/BGM/MysticalBGM");
        RelaxingPianoBGM = Resources.Load<AudioClip>("Sounds/BGM/RelaxingPianoBGM");
        CalmPianoBGM = Resources.Load<AudioClip>("Sounds/BGM/CalmPianoBGM");
        ShootingStarBGM = Resources.Load<AudioClip>("Sounds/BGM/ShootingStarBGM");


        //SFX
        ClearSFX = Resources.Load<AudioClip>("Sounds/SFX/ClearSFX");
        FireBallSFX = Resources.Load<AudioClip>("Sounds/SFX/FireBallSFX");
        ItemPickUpSFX = Resources.Load<AudioClip>("Sounds/SFX/ItemPickUpSFX");
        PunchSFX = Resources.Load<AudioClip>("Sounds/SFX/PunchSFX");
        SwordSFX = Resources.Load<AudioClip>("Sounds/SFX/SwordSFX");
        WinningSFX = Resources.Load<AudioClip>("Sounds/SFX/WinningSFX");
        ArrowSFX = Resources.Load<AudioClip>("Sounds/SFX/ArrowSFX");
        DefenseSFX = Resources.Load<AudioClip>("Sounds/SFX/DefenseSFX");
        RollSFX = Resources.Load<AudioClip>("Sounds/SFX/RollSFX");
        BookSFX = Resources.Load<AudioClip>("Sounds/SFX/BookSFX");
        Button1SFX = Resources.Load<AudioClip>("Sounds/SFX/Button1SFX");
        Button2SFX = Resources.Load<AudioClip>("Sounds/SFX/Button2SFX");
        SuccessSFX = Resources.Load<AudioClip>("Sounds/SFX/SuccessSFX");
        FailSFX = Resources.Load<AudioClip>("Sounds/SFX/FailSFX");
    }

    // 미리 설정된 BGM을 재생하는 메서드
    public void PlayStartBGMAsiaTravel() => PlayBGM(AsiaTravelBGM);
    public void PlayStartBGMSciFiMoodtimeflow() => PlayBGM(MoodtimeflowBGM);
    public void PlayStartBGMMystical() => PlayBGM(MysticalBGM);
    public void PlayStartBGMRelaxingPiano() => PlayBGM(RelaxingPianoBGM);
    public void PlayStartBGMCalmPiano() => PlayBGM(CalmPianoBGM);
    public void PlayStartBGMShootingStar() => PlayBGM(ShootingStarBGM);

    // 미리 설정된 SFX를 재생하는 메서드 SoundManager.Instance.PlayClearSFX();
    public void PlayClearSFX() => PlaySFX(ClearSFX);
    public void PlayFireBallSFX() => PlaySFX(FireBallSFX);
    public void PlayItemPickUpSFX() => PlaySFX(ItemPickUpSFX);
    public void PlayPunchSFX() => PlaySFX(PunchSFX);
    public void PlaySwordSFX() => PlaySFX(SwordSFX);
    public void PlayWinningSFX() => PlaySFX(WinningSFX);
    public void PlayArrowSFX() => PlaySFX(ArrowSFX);
    public void PlayDefenseSFX() => PlaySFX(DefenseSFX);
    public void PlayRollSFX() => PlaySFX(RollSFX);
    public void PlayBookSFX() => PlaySFX(BookSFX);
    public void PlayButton1SFX() => PlaySFX(Button1SFX);//NormalButton
    public void PlayButton2SFX() => PlaySFX(Button2SFX);//SystemButton

    public void PlaySuccessSFX() => PlaySFX(SuccessSFX);
    public void PlayFailSFX() => PlaySFX(FailSFX);




    public void PlayBGM(AudioClip clip)
    {
        // BGM이 바뀌면 새로운 BGM을 재생
        if (_bgmSource.clip != clip)
        {
            _bgmSource.clip = clip;
            _bgmSource.loop = true;
            _bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        // SFX를 재생할 최소 간격을 두고 재생
        if (Time.time - lastSFXTime >= minSFXInterval)
        {
            _sfxSource.PlayOneShot(clip);
            lastSFXTime = Time.time; // 마지막 재생 시간 갱신
        }
    }
    public void SetMuteBGM()
    {
        _bgmSource.mute = !_bgmSource.mute;
    }
    public void SetMuteSFX()
    {
        _sfxSource.mute = !_sfxSource.mute;
    }





    // BGM의 볼륨을 가져오는 메서드
    public float GetBGMVolume() => _bgmSource.volume;

    // BGM의 볼륨을 설정하는 메서드
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;

    // SFX의 볼륨을 가져오는 메서드
    public float GetSFXVolume() => _sfxSource.volume;

    // SFX의 볼륨을 설정하는 메서드
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}