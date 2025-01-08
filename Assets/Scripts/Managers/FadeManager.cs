using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadePanel; // Panel�� Image ������Ʈ
    public float fadeDuration = 3f; // ���̵� ���� �ð�

    private void Awake()
    {
        fadePanel = GetComponentInChildren<Image>();
    }
    // ���� ��ȯ�ϸ� ���̵� ȿ�� ����
    public void LoadSceneWithFade(string sceneName)// �� �̵��Ҷ� ȣ��
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // ȭ�� ��Ӱ� (���̵� ��)
        yield return StartCoroutine(Fade(1f));
        // �� �ε�
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        // ȭ�� ��� (���̵� �ƿ�)
        yield return StartCoroutine(Fade(0f));
    }

    // ���̵� ȿ�� ����
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

        // ������ ���� �� ����
        color.a = targetAlpha;
        fadePanel.color = color;
    }
}