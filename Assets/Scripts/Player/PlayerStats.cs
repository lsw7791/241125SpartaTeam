using UnityEngine.PlayerLoop;

[System.Serializable]
public class PlayerStats
{
    public int MaxHP;               // HP
    public int CurrentHP;               // HP
    public int Stamina;          // Stamina
    public int Gold;             // Gold
    public int Damage;           // Damage
    public int Speed;            // Speed
    public float AttackSpeed;    // Attack Speed
    public int Defense;          // Defense
    public int WeaponType;       // Weapon Type (¿¹: 0 = Sword, 1 = Bow µî)
    public bool isDie;

    public void Initialize()
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
        Stamina = 100;
        Gold = 0;
        Damage = 10;
        Speed = 1;
        AttackSpeed = 1;
        Defense = 1;
        WeaponType = 0;
        isDie = false;
    }
}
