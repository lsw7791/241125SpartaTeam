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
        _maxHP.text = GameManager.Instance.Player.stats.MaxHP.ToString();
        _maxStamina.text = GameManager.Instance.Player.stats.MaxStamina.ToString();
        _aTKPhysic.text = GameManager.Instance.Player.stats.PhysicalDamage.ToString();
        _aTKMagic.text = GameManager.Instance.Player.stats.MagicalDamage.ToString();
        _aTKMine.text = GameManager.Instance.Player.stats.MineDamage.ToString();
        _def.text = GameManager.Instance.Player.stats.Def.ToString();
        _atkSpd.text = GameManager.Instance.Player.stats.ATKSpeed.ToString();
        _spd.text = GameManager.Instance.Player.stats.MoveSpeed.ToString();
    }
}
