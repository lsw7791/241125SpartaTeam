using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 10f;  // ����ü ���� �ð�
    public int damage;      // ����ü ������

    public LayerMask targetLayer; // ���� ��� ���̾��ũ

    public void Initialize(LayerMask inTargetLayer)
    {
        targetLayer = inTargetLayer; // Ÿ�� ���̾� ����
    }

    private void Start()
    {
        //Invoke("DeSpenObject", lifeTime);
        Destroy(gameObject, lifeTime);
    }

    void DeSpenObject()
    {
        // TODO :: ID�� ����
        //GameManager.Instance.projectilePool.ReturnProjectile(gameObject.name, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // targetLayer�� ���ԵǴ� ���̾����� Ȯ��
        if (IsLayerMatched(targetLayer.value, collision.gameObject.layer))
        {
            // TODO :: ������ ������ �÷��̾� �ʿ��� �Ѵٰ� ��
            if (collision.TryGetComponent<IDamageable>(out var outtarget))
            { // ���̾ ��ġ�� �Ա� ������ ���Ϳ� �÷��̾��� ȥ���� ����
                outtarget.TakeDamage(damage); // ������ ó��
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
