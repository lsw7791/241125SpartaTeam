using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private GameObject _bgmObj;
    private GameObject _sfxObj;

    public AudioSource _bgmSource;
    public AudioSource _sfxSource;

    [Header("BGM")]
    [SerializeField] private AudioClip asiaTravel118018;
    [SerializeField] private AudioClip sciFiMoodtimeflow194382;
    [SerializeField] private AudioClip mysticalStrangerThings133254;

    [Header("SFX")]
    [SerializeField] private AudioClip efekSound3220030;
    [SerializeField] private AudioClip achievementVideoGameType1230515;
    [SerializeField] private AudioClip fireTorchWhoosh2186586;
    [SerializeField] private AudioClip gameStart6104;
    [SerializeField] private AudioClip itemPickUp38258;
    [SerializeField] private AudioClip punch140236;
    [SerializeField] private AudioClip swordSoundEffect1234987;
    [SerializeField] private AudioClip winning218995;

    protected override void Awake()
    {
        base.Awake();
        SetAudioSource();
        SetAudioClip();

    }
    private void Start()
    {
        _bgmSource.volume = 0.3f;
        _sfxSource.volume = 0.3f;
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
        sciFiMoodtimeflow194382 = Resources.Load<AudioClip>("Sounds/BGM/sci-fi-moodtimeflow-194382");
        asiaTravel118018 = Resources.Load<AudioClip>("Sounds/BGM/asia-travel-118018");
        mysticalStrangerThings133254 = Resources.Load<AudioClip>("Sounds/BGM/80s-mystical-stranger-things-133254");


        //SFX
        efekSound3220030 = Resources.Load<AudioClip>("Sounds/SFX/1-efek-sound-3-220030");
        achievementVideoGameType1230515 = Resources.Load<AudioClip>("Sounds/SFX/achievement-video-game-type-1-230515");
        fireTorchWhoosh2186586 = Resources.Load<AudioClip>("Sounds/SFX/fire-torch-whoosh-2-186586");
        gameStart6104 = Resources.Load<AudioClip>("Sounds/SFX/game-start-6104");
        itemPickUp38258 = Resources.Load<AudioClip>("Sounds/SFX/item-pick-up-38258");
        punch140236 = Resources.Load<AudioClip>("Sounds/SFX/punch-140236");
        swordSoundEffect1234987 = Resources.Load<AudioClip>("Sounds/SFX/sword-sound-effect-1-234987");
        winning218995 = Resources.Load<AudioClip>("Sounds/SFX/winning-218995");


    }

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
        // SFX를 한 번만 재생
        _sfxSource.PlayOneShot(clip);
    }
    public void SetMuteBGM()
    {
        _bgmSource.mute = !_bgmSource.mute;
    }
    public void SetMuteSFX()
    {
        _sfxSource.mute = !_sfxSource.mute;
    }
    // 미리 설정된 BGM을 재생하는 메서드//BGM
    public void PlayStartBGMAsiaTravel() => PlayBGM(asiaTravel118018);
    public void PlayStartBGMSciFiMoodtimeflow() => PlayBGM(sciFiMoodtimeflow194382);
    public void PlayStartBGMMystical() => PlayBGM(mysticalStrangerThings133254);



    // 미리 설정된 충돌 SFX를 재생하는 메서드
    public void PlayCollsionSFX() => PlaySFX(punch140236);

    // 미리 설정된 클릭 SFX를 재생하는 메서드
    public void PlayClickSFX() => PlaySFX(gameStart6104);



    // BGM의 볼륨을 가져오는 메서드
    public float GetBGMVolume() => _bgmSource.volume;

    // BGM의 볼륨을 설정하는 메서드
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;

    // SFX의 볼륨을 가져오는 메서드
    public float GetSFXVolume() => _sfxSource.volume;

    // SFX의 볼륨을 설정하는 메서드
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}