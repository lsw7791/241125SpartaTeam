using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private float _lifeTime = 10f;  // ����ü ���� �ð�
    [SerializeField]
    private int _damage;      // ����ü ������
   // [SerializeField]
   // private int _mineDamage = 100;      // ä�� ������
                                        // TODO :: ������ �� �޾ƿ���

    [SerializeField]
    private LayerMask _targetLayer; // ���� ��� ���̾��ũ
    [SerializeField]
    private LayerMask _obstacleLayer; //  ��ֹ� ���̾��ũ

    public void Initialize(LayerMask inTargetLayer, LayerMask inObstacleLayer, int inDamage)
    {
        _targetLayer = inTargetLayer; // Ÿ�� ���̾� ����
        _obstacleLayer = inObstacleLayer;
        _damage = inDamage;
    }

    private void OnEnable()
    {
        Invoke("DeSpenObject", _lifeTime);
        //Destroy(gameObject, _lifeTime);
    }

    private void DeSpenObject()
    {
        // TODO :: ID�� ����
        //ParticleSystem particleSystem = GameManager.Instance.EffectParticle;

        //particleSystem.transform.position = gameObject.transform.position;
        //ParticleSystem.EmissionModule em = particleSystem.emission;
        //em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(attackData.size * 5)));
        //ParticleSystem.MainModule mm = particleSystem.main;
        //mm.startSpeedMultiplier = attackData.size * 10f;
        //particleSystem.Play();
        GameManager.Instance.attackEffect.ApplyAttackEffect(gameObject.transform.position);
        gameObject.SetActive(false);
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
            DeSpenObject();      // ����ü �ı�
        }
        else if(IsLayerMatched(_obstacleLayer.value, collision.gameObject.layer))
        {
            DeSpenObject();      // ����ü �ı�
        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ���ϴ� �޼ҵ�
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
