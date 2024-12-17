//using Unity.VisualScripting.Antlr3.Runtime.Misc;

//[System.Serializable]
//public class PlayerStatsData
//{
//    public int MaxHP;
//    public int CurrentHP;
//    public int Stamina;
//    public int Gold;
//    public int Damage;
//    public int Speed;
//    public float AttackSpeed;
//    public int Defense;
//    public int WeaponType;

//    // 게임 상태를 데이터로 변환
//    public static PlayerStatsData FromPlayerStats(PlayerStats stats)
//    {
//        return new PlayerStatsData
//        {
//            MaxHP = stats.MaxHP,
//            CurrentHP = stats.CurrentHP,
//            Stamina = stats.Stamina,
//            Gold = stats.Gold,
//            Damage = stats.Damage,
//            Speed = stats.Speed,
//            AttackSpeed = stats.AttackSpeed,
//            Defense = stats.Defense,
//            WeaponType = stats.WeaponType
//        };
//    }

//    // 데이터에서 게임 상태로 변환
//    public static PlayerStats ToPlayerStats(PlayerStatsData data)
//    {
//        return new PlayerStats
//        {
//            MaxHP = data.MaxHP,
//            CurrentHP = data.CurrentHP,
//            Stamina = data.Stamina,
//            Gold = data.Gold,
//            Damage = data.Damage,
//            Speed = data.Speed,
//            AttackSpeed = data.AttackSpeed,
//            Defense = data.Defense,
//            WeaponType = data.WeaponType
//        };
//    }
//}
