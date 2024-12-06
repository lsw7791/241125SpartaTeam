using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private float _lifeTime = 10f;  // ����ü ���� �ð�
    [SerializeField]
    private int _damage;      // ����ü ������
    [SerializeField]
    private int _mineDamage = 100;      // ä�� ������
                                        // TODO :: ������ �� �޾ƿ���

    [SerializeField]
    private LayerMask _targetLayer; // ���� ��� ���̾��ũ

    public void Initialize(LayerMask inTargetLayer, int inDamage)
    {
        _targetLayer = inTargetLayer; // Ÿ�� ���̾� ����
        _damage = inDamage;
    }

    private void Start()
    {
        //Invoke("DeSpenObject", lifeTime);
        Destroy(gameObject, _lifeTime);
    }

    private void DeSpenObject()
    {
        // TODO :: ID�� ����
        //SpawnManager.Instance.projectilePool.ReturnProjectile(gameObject.GetType().Name, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // targetLayer�� ���ԵǴ� ���̾����� Ȯ��
        if (IsLayerMatched(_targetLayer.value, collision.gameObject.layer))
        {
            if (collision.TryGetComponent<IDamageable>(out var outPlayer))
            { // ���̾ ��ġ�� �Ա� ������ ���Ϳ� �÷��̾��� ȥ���� ����
                outPlayer.TakeDamage(_damage); // ������ ó��
            }
            else if (collision.transform.parent.TryGetComponent<ICreature>(out var outEnemy))
            {
                outEnemy.TakeDamage(_damage); // ������ ó��
            }
            // ���� ���̾ �߰��� �߻�ü ����
            //DeSpenObject();      // ����ü �ı�

            Destroy(gameObject);
        }
        else if (collision.transform.parent != null && collision.transform.parent.TryGetComponent<ICreature>(out var outMine))
        { // ���ʹ� ���� ���� ����� ä�� �������� �����Ű����ϱ�
            outMine.TakeDamage(_mineDamage); // ������ ó��
            Destroy(gameObject);
        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ���ϴ� �޼ҵ�
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
