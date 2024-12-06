using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private GameObject _bgmObj;
    private GameObject _sfxObj;

    public AudioSource _bgmSource;
    public AudioSource _sfxSource;

    [Header("BGM")]
    [SerializeField] private AudioClip bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioClip collisionSfx;
    [SerializeField] private AudioClip clickSfx;
    [SerializeField] private AudioClip itemSfx;

    private void Start()
    {
        SetAudioSource();
        SetAudioClip();
        _bgmSource.volume = 0.5f;
        _sfxSource.volume = 0.5f;
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
        // Resources 폴더에서 AudioClip을 불러옵니다.
        bgmClip = Resources.Load<AudioClip>("Prefabs/Sounds/ambient-game-67014");
        collisionSfx = Resources.Load<AudioClip>("Prefabs/Sounds/game-start-6104");
        clickSfx = Resources.Load<AudioClip>("Prefabs/Sounds/game-start-6104");
        itemSfx = Resources.Load<AudioClip>("Prefabs/Sounds/collect-ring-15982");
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

    // 미리 설정된 BGM을 재생하는 메서드
    public void PlayStartBGM() => PlayBGM(bgmClip);

    // 미리 설정된 충돌 SFX를 재생하는 메서드
    public void PlayCollsionSFX() => PlaySFX(collisionSfx);

    // 미리 설정된 클릭 SFX를 재생하는 메서드
    public void PlayClickSFX() => PlaySFX(clickSfx);

    // BGM의 볼륨을 가져오는 메서드
    public float GetBGMVolume() => _bgmSource.volume;

    // BGM의 볼륨을 설정하는 메서드
    public void SetBGMVolume(float volume) => _bgmSource.volume = volume;

    // SFX의 볼륨을 가져오는 메서드
    public float GetSFXVolume() => _sfxSource.volume;

    // SFX의 볼륨을 설정하는 메서드
    public void SetSFXVolume(float volume) => _sfxSource.volume = volume;
}