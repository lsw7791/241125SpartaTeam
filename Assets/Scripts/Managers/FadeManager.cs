using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadePanel; // Panel의 Image 컴포넌트
    public float fadeDuration = 3f; // 페이드 지속 시간

    private void Awake()
    {
        fadePanel = GetComponentInChildren<Image>();
    }
    // 씬을 전환하며 페이드 효과 실행
    public void LoadSceneWithFade(string sceneName)// 씬 이동할때 호출
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // 화면 어둡게 (페이드 인)
        yield return StartCoroutine(Fade(1f));
        // 씬 로드
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        // 화면 밝게 (페이드 아웃)
        yield return StartCoroutine(Fade(0f));
    }

    // 페이드 효과 실행
    private IEnumerator Fade(float targetAlpha)
    {
        Color color = fadePanel.color;
        float startAlpha = color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            fadePanel.color = color;
            yield return null;
        }

        // 마지막 알파 값 설정
        color.a = targetAlpha;
        fadePanel.color = color;
    }
}