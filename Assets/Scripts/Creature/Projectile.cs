using UnityEngine;

public class Projectile : MonoBehaviour
{
    // TODO :: ������ �����Ϳ��� �������� �� �޾ƿ���
    public float lifeTime = 3f;  // ����ü ���� �ð�
    public int damage = 10;      // ����ü ������

    public LayerMask targetLayer; // ���� ��� ���̾��ũ

    public void Initialize(LayerMask inTargetLayer)
    {
        targetLayer = inTargetLayer; // Ÿ�� ���̾� ����
    }

    private void Start()
    {
        //Invoke("DeSpenObject", lifeTime);
        Destroy(gameObject, 10);
    }

    void DeSpenObject()
    {
        //GameManager.Instance.projectilePool.ReturnProjectile(gameObject.name, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // targetLayer�� ���ԵǴ� ���̾����� Ȯ��
        if (IsLayerMatched(targetLayer.value, collision.gameObject.layer))
        {
            if (collision.TryGetComponent<IDamageable>(out var outTarget))
            { // ���̾ ��ġ�� �Ա� ������ ���Ϳ� �÷��̾��� ȥ���� ����
                outTarget.TakeDamage(damage); // ������ ó��
            }
            // ���� ���̾ �߰��� �߻�ü ����
            //DeSpenObject();      // ����ü �ı�
            Destroy(gameObject);
        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ���ϴ� �޼ҵ�
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
