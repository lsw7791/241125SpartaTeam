using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _weapon; // 현재 무기를 나타내는 오브젝트
    [SerializeField] private GameObject _newWeapon; // 새로 장착할 무기 프리팹

    private void Awake()
    {
        _weapon = this.gameObject;
        _weapon = EquipManager.Instance.WeaponObject;
    }
    
   
}
