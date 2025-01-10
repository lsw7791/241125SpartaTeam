using UnityEngine;

public class DamageUIController : MonoBehaviour
{
    public GameObject damageTextPrefab; // Damage �ؽ�Ʈ ������
    private Transform canvasTransform;  // Damage �ؽ�Ʈ�� ǥ���� Canvas
    private void Awake()
    {
        canvasTransform = GetComponentInChildren<Canvas>().transform;
    }
    public void ShowDamage(Vector2 worldPosition, int damage)
    {
        // DamageText ������Ʈ ����
        GameObject damageTextObj = Instantiate(damageTextPrefab, canvasTransform);

        // DamageText�� RectTransform ��������
        RectTransform damageTextRect = damageTextObj.GetComponent<RectTransform>();

        // DamageText�� ���� ��ǥ�� ���� ����
        damageTextRect.position = worldPosition;

        // �ؽ�Ʈ ���� ����
        var textMesh = damageTextObj.GetComponent<TMPro.TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = damage.ToString();
        }

        // �ణ�� �Ӹ� �� ������ �߰�
        Vector3 offset = new Vector3(0, 0.05f, 0); // �Ӹ� ���� �̵�
        damageTextRect.position += offset;
    }
}