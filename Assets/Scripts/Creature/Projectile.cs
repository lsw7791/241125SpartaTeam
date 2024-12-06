using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 10f;  // 투사체 생존 시간
    public int damage;      // 투사체 데미지

    public LayerMask targetLayer; // 감지 대상 레이어마스크

    public void Initialize(LayerMask inTargetLayer)
    {
        targetLayer = inTargetLayer; // 타겟 레이어 설정
    }

    private void Start()
    {
        //Invoke("DeSpenObject", lifeTime);
        Destroy(gameObject, lifeTime);
    }

    void DeSpenObject()
    {
        // TODO :: ID로 구현
        //GameManager.Instance.projectilePool.ReturnProjectile(gameObject.name, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // targetLayer에 포함되는 레이어인지 확인
        if (IsLayerMatched(targetLayer.value, collision.gameObject.layer))
        {
            // TODO :: 데미지 관련은 플레이어 쪽에서 한다고 함
            if (collision.TryGetComponent<IDamageable>(out var outtarget))
            { // 레이어를 거치고 왔기 때문에 몬스터와 플레이어의 혼동은 없음
                outtarget.TakeDamage(damage); // 데미지 처리
            }
            // 벽도 레이어를 추가해 발사체 제거
            //DeSpenObject();      // 투사체 파괴
            Destroy(gameObject);
        }
    }

    // 레이어가 일치하는지 확인하는 메소드
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
