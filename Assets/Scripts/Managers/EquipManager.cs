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


    private SpriteRenderer _weaponSpriteRenderer; // WeaponObject�� SpriteRenderer

    private void Awake()
    {
        // WeaponObject�� SpriteRenderer ������Ʈ�� ��������
        _weaponSpriteRenderer = WeaponObject.GetComponent<SpriteRenderer>();

        // ������ ���Ⱑ ���� ���, ��������Ʈ�� null�� ����
        if (EquipedWeapon == null)
        {
            SetWeaponSprite(null); // ��������Ʈ�� null�� �����Ͽ� "�Ǽ�" ���·� ����
        }
        else
        {
            SetWeaponSprite(EquipedWeapon); // ������ ������ ��������Ʈ�� ����
        }
    }

    // ���ο� ���� ����
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        EquipedWeapon = newWeaponPrefab; // �� ���⸦ ����

        // ���ο� ������ ��������Ʈ�� ����
        SetWeaponSprite(EquipedWeapon);
    }

    // ��������Ʈ ���� ó��
    private void SetWeaponSprite(GameObject weaponPrefab)
    {
        if (weaponPrefab == null)
        {
            _weaponSpriteRenderer.sprite = null; // ���� ���� ��� ��������Ʈ�� null�� ���� (�Ǽ� ����)
        }
        else
        {
            SpriteRenderer weaponSpriteRenderer = weaponPrefab.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer.sprite = weaponSpriteRenderer.sprite; // WeaponObject�� ��������Ʈ ����
        }
    }
}
