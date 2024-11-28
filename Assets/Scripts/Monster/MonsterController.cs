using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Monster monster;

    [Header("Condition")]
    [SerializeField]
    private int health;

    private void Start()
    {
        //GameManager.Instance.플레이어.공격이벤트 += HitDamage;
    }

    private void OnEnable()
    {
        monster = GetComponentInParent<Monster>();

        Initialize();
    }

    private void Initialize()
    {
        health = monster.Health;
    }

    public void HitDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        //CreateManager.Instance.ReturnObjectToPool(gameObject);
        monster.gameObject.SetActive(false);
        monster.isDie = true;
    }

    // TODO :: TestTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TopDownController>(out var outPlayer))
            HitDamage(1);
    }
}
