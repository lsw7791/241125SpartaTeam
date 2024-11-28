using UnityEngine;

public class Monster : MonoBehaviour
{
    // Inspector에서 값을 보이도록 설정
    [SerializeField] private string monsterName;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;

    // public getter로 값을 접근할 수 있도록 하여 외부에서 사용할 수 있도록 함
    public string MonsterName => monsterName;
    public int Health => health;
    public float Speed => speed;
    public int Damage => damage;
    public float DetectionRange => detectionRange;
    public float AttackRange => attackRange;

    public bool isDie = false;

    // Initialize 메서드에서 값을 설정
    public void Initialize(MonsterData data)
    {
        monsterName = data.Name;
        health = data.Health;
        speed = data.Speed;
        damage = data.Damage;
        detectionRange = data.DetectionRange;
        attackRange = data.AttackRange;
    }
}