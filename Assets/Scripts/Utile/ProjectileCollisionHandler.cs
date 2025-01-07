using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private float _lifeTime = 10f;  // 투사체 생존 시간
    [SerializeField]
    private int _damage;      // 투사체 데미지
   // [SerializeField]
   // private int _mineDamage = 100;      // 채광 데미지
                                        // TODO :: 광석도 값 받아오기

    [SerializeField]
    private LayerMask _targetLayer; // 감지 대상 레이어마스크
    [SerializeField]
    private LayerMask _obstacleLayer; //  장애물 레이어마스크

    public void Initialize(LayerMask inTargetLayer, LayerMask inObstacleLayer, int inDamage)
    {
        _targetLayer = inTargetLayer; // 타겟 레이어 설정
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
        // TODO :: ID로 구현
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
        // targetLayer에 포함되는 레이어인지 확인
        if (IsLayerMatched(_targetLayer.value, collision.gameObject.layer))
        {
            if (collision.TryGetComponent<IDamageable>(out var outPlayer))
            { // 레이어를 거치고 왔기 때문에 몬스터와 플레이어의 혼동은 없음
                outPlayer.TakeDamage(_damage); // 데미지 처리
            }
            else if (collision.transform.parent.TryGetComponent<ICreature>(out var outEnemy))
            {
                outEnemy.TakeDamage(_damage); // 데미지 처리
            }
            DeSpenObject();      // 투사체 파괴
        }
        else if(IsLayerMatched(_obstacleLayer.value, collision.gameObject.layer))
        {
            DeSpenObject();      // 투사체 파괴
        }
    }

    // 레이어가 일치하는지 확인하는 메소드
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
