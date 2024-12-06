using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private float _lifeTime = 10f;  // 투사체 생존 시간
    [SerializeField]
    private int _damage;      // 투사체 데미지
    [SerializeField]
    private int _mineDamage = 100;      // 채광 데미지
                                        // TODO :: 광석도 값 받아오기

    [SerializeField]
    private LayerMask _targetLayer; // 감지 대상 레이어마스크

    public void Initialize(LayerMask inTargetLayer, int inDamage)
    {
        _targetLayer = inTargetLayer; // 타겟 레이어 설정
        _damage = inDamage;
    }

    private void Start()
    {
        //Invoke("DeSpenObject", lifeTime);
        Destroy(gameObject, _lifeTime);
    }

    private void DeSpenObject()
    {
        // TODO :: ID로 구현
        //SpawnManager.Instance.projectilePool.ReturnProjectile(gameObject.GetType().Name, gameObject);
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
            // 벽도 레이어를 추가해 발사체 제거
            //DeSpenObject();      // 투사체 파괴

            Destroy(gameObject);
        }
        else if (collision.transform.parent != null && collision.transform.parent.TryGetComponent<ICreature>(out var outMine))
        { // 몬스터는 돌진 몬스터 빼고는 채광 데미지가 없을거같으니까
            outMine.TakeDamage(_mineDamage); // 데미지 처리
            Destroy(gameObject);
        }
    }

    // 레이어가 일치하는지 확인하는 메소드
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
