using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ
    [SerializeField] private GameObject _newWeapon; // ���� ������ ���� ������

    private void Awake()
    {
        _weapon = this.gameObject;
        _weapon = EquipManager.Instance.WeaponObject;
    }
    
   
}
