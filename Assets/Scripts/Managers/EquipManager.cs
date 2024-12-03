using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoSingleton<EquipManager>
{
    public GameObject WeaponObject;
    public GameObject EquipedWeapon;
    //public GameObject _equipedHelmet;
    //public GameObject _equipedTop;
    //public GameObject _equipedArmor;
    //public GameObject _equipedBottom;
    //public GameObject _equipedCape;
    //public GameObject _equipedShield;


    private SpriteRenderer _weaponSpriteRenderer; // WeaponObject의 SpriteRenderer

    private void Awake()
    {
        // WeaponObject의 SpriteRenderer 컴포넌트를 가져오기
        _weaponSpriteRenderer = WeaponObject.GetComponent<SpriteRenderer>();

        // 장착된 무기가 없을 경우, 스프라이트를 null로 설정
        if (EquipedWeapon == null)
        {
            SetWeaponSprite(null); // 스프라이트를 null로 설정하여 "맨손" 상태로 만듦
        }
        else
        {
            SetWeaponSprite(EquipedWeapon); // 장착된 무기의 스프라이트를 설정
        }
    }

    // 새로운 무기 장착
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        EquipedWeapon = newWeaponPrefab; // 새 무기를 장착

        // 새로운 무기의 스프라이트를 적용
        SetWeaponSprite(EquipedWeapon);
    }

    // 스프라이트 변경 처리
    private void SetWeaponSprite(GameObject weaponPrefab)
    {
        if (weaponPrefab == null)
        {
            _weaponSpriteRenderer.sprite = null; // 무기 없을 경우 스프라이트를 null로 설정 (맨손 상태)
        }
        else
        {
            SpriteRenderer weaponSpriteRenderer = weaponPrefab.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer.sprite = weaponSpriteRenderer.sprite; // WeaponObject의 스프라이트 변경
        }
    }
}
