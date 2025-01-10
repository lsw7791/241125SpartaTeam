using UnityEngine;

public class DamageUIController : MonoBehaviour
{
    public GameObject damageTextPrefab; // Damage 텍스트 프리팹
    private Transform canvasTransform;  // Damage 텍스트를 표시할 Canvas
    private void Awake()
    {
        canvasTransform = GetComponentInChildren<Canvas>().transform;
    }
    public void ShowDamage(Vector2 worldPosition, int damage)
    {
        // DamageText 오브젝트 생성
        GameObject damageTextObj = Instantiate(damageTextPrefab, canvasTransform);

        // DamageText의 RectTransform 가져오기
        RectTransform damageTextRect = damageTextObj.GetComponent<RectTransform>();

        // DamageText의 월드 좌표를 직접 설정
        damageTextRect.position = worldPosition;

        // 텍스트 내용 설정
        var textMesh = damageTextObj.GetComponent<TMPro.TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = damage.ToString();
        }

        // 약간의 머리 위 오프셋 추가
        Vector3 offset = new Vector3(0, 0.05f, 0); // 머리 위로 이동
        damageTextRect.position += offset;
    }
}