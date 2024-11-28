public class MonsterData
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }

    public float DetectionRange { get; private set; }
    public float AttackRange { get; private set; }

    public MonsterData(string name, int health, float speed, int damage, float detectionRange, float attackRange)
    {
        Name = name;
        Health = health;
        Speed = speed;
        Damage = damage;
        DetectionRange = detectionRange;
        AttackRange = attackRange;
    }
}
