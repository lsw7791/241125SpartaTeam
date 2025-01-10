using UnityEngine;
using TMPro;

public class FadeAndMove : MonoBehaviour
{
    public float moveSpeed = 100f;      // ���� �̵� �ӵ�
    public float fadeDuration = 10f;   // ���̵� �ƿ� ���� �ð�

    private TextMeshProUGUI textMesh; // TextMeshPro ������Ʈ
    private Color originalColor;      // �ʱ� ����
    private float elapsedTime = 0f;   // ��� �ð�

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
        // �ؽ�Ʈ ���� �̵� (���û���)
        transform.Translate(new Vector2(0f, moveSpeed * Time.deltaTime));
    }
    // ���� �� ������ FixedUpdate���� ó��
    private void FixedUpdate()
    {
        if (textMesh != null)
        {
            elapsedTime += Time.deltaTime;  // ��� �ð� ����

            // ���� �� ��� (��� �ð��� fadeDuration���� ���� ������ ����)
            float alpha = Mathf.Lerp(originalColor.a, 0f, Mathf.Min(elapsedTime / fadeDuration, 1f));
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // ���� ���� 0�� ��������� ����
            if (textMesh.color.a <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}