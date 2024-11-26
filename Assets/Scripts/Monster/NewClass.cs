using UnityEngine;

public class Monster : MonoBehaviour
{
    public string MonsterName { get; private set; }
    public int Health { get; private set; }
    public float Speed { get; private set; }
    public int Damage { get; private set; }

    public void Initialize(MonsterData data)
    {
        MonsterName = data.Name;
        Health = data.Health;
        Speed = data.Speed;
        Damage = data.Damage;
    }
}
