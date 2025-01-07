using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusUI : UIBase
{
    [Header("StatsValue")]
    [SerializeField] TMP_Text _maxHP;
    [SerializeField] TMP_Text _maxStamina;
    [SerializeField] TMP_Text _aTKPhysic;
    [SerializeField] TMP_Text _aTKMagic;
    [SerializeField] TMP_Text _aTKMine;
    [SerializeField] TMP_Text _def;
    [SerializeField] TMP_Text _atkSpd;
    [SerializeField] TMP_Text _spd;

    protected override void Awake()
    {
        base.Awake(); 
        Refresh();
    }

    public void Refresh()
    {
        PlayerData nowPlayerData = GameManager.Instance.nowPlayer;

        _maxHP.text = nowPlayerData.MaxHP.ToString();
        _maxStamina.text = nowPlayerData.MaxStamina.ToString();
        _aTKPhysic.text = nowPlayerData.PhysicalDamage.ToString();
        _aTKMagic.text = nowPlayerData.MagicalDamage.ToString();
        _aTKMine.text = nowPlayerData.MineDamage.ToString();
        _def.text = nowPlayerData.Def.ToString();
        _atkSpd.text = nowPlayerData.ATKSpeed.ToString();
        _spd.text = nowPlayerData.MoveSpeed.ToString();
    }
}
