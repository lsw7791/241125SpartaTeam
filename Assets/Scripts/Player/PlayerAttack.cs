using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ
    private SpriteRenderer _weaponSpriteRenderer; // _weapon ������Ʈ�� SpriteRenderer

    [SerializeField] private GameObject _newWeapon; // ���� ������ ���� ������

    private void Awake()
    {
        _weapon = this.gameObject;
        _weaponSpriteRenderer = _weapon.GetComponent<SpriteRenderer>(); // weapon�� SpriteRenderer ��������
    }
    
    // ���ο� ���⸦ �����ϰ� weapon�� ��������Ʈ�� ��ü
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        _newWeapon = newWeaponPrefab; // ���ο� ���� ������Ʈ ����
        SpriteRenderer newWeaponSpriteRenderer = _newWeapon.GetComponent<SpriteRenderer>();

        // weapon�� ��������Ʈ�� ���ο� ������ ��������Ʈ�� ��ü
        _weaponSpriteRenderer.sprite = newWeaponSpriteRenderer.sprite;
    }
}
