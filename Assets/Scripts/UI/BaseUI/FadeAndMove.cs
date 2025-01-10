using UnityEngine;
using TMPro;

public class FadeAndMove : MonoBehaviour
{
    public float moveSpeed = 100f;      // 위로 이동 속도
    public float fadeDuration = 10f;   // 페이드 아웃 지속 시간

    private TextMeshProUGUI textMesh; // TextMeshPro 컴포넌트
    private Color originalColor;      // 초기 색상
    private float elapsedTime = 0f;   // 경과 시간

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh != null)
        {
            originalColor = textMesh.color;
        }
    }
    private void Update()
    {
        // 텍스트 위로 이동 (선택사항)
        transform.Translate(new Vector2(0f, moveSpeed * Time.deltaTime));
    }
    // 알파 값 조정은 FixedUpdate에서 처리
    private void FixedUpdate()
    {
        if (textMesh != null)
        {
            elapsedTime += Time.deltaTime;  // 경과 시간 증가

            // 알파 값 계산 (경과 시간을 fadeDuration으로 나눈 값으로 보간)
            float alpha = Mathf.Lerp(originalColor.a, 0f, Mathf.Min(elapsedTime / fadeDuration, 1f));
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // 알파 값이 0에 가까워지면 삭제
            if (textMesh.color.a <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}