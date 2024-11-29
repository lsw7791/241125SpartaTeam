using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Inspector���� ���� ���̵��� ����
    [SerializeField] private string monsterName;
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float detectionRange;
    [SerializeField] private float attackRange;

    // public getter�� ���� ������ �� �ֵ��� �Ͽ� �ܺο��� ����� �� �ֵ��� ��
    public string MonsterName => monsterName;
    public int Health => health;
    public float Speed => speed;
    public int Damage => damage;
    public float DetectionRange => detectionRange;
    public float AttackRange => attackRange;

    public bool isDie = false;

    // Initialize �޼��忡�� ���� ����
    //public void Initialize(MonsterData data)
    //{
    //    monsterName = data.Name;
    //    health = data.Health;
    //    speed = data.Speed;
    //    damage = data.Damage;
    //    detectionRange = data.DetectionRange;
    //    attackRange = data.AttackRange;
    //}
}
