using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _weapon; // 현재 무기를 나타내는 오브젝트
    private SpriteRenderer _weaponSpriteRenderer; // _weapon 오브젝트의 SpriteRenderer

    [SerializeField] private GameObject _newWeapon; // 새로 장착할 무기 프리팹

    private void Awake()
    {
        _weapon = this.gameObject;
        _weaponSpriteRenderer = _weapon.GetComponent<SpriteRenderer>(); // weapon의 SpriteRenderer 가져오기
    }
    
    // 새로운 무기를 장착하고 weapon의 스프라이트를 교체
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        _newWeapon = newWeaponPrefab; // 새로운 무기 오브젝트 설정
        SpriteRenderer newWeaponSpriteRenderer = _newWeapon.GetComponent<SpriteRenderer>();

        // weapon의 스프라이트를 새로운 무기의 스프라이트로 교체
        _weaponSpriteRenderer.sprite = newWeaponSpriteRenderer.sprite;
    }
}
