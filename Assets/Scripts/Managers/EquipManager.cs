using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoSingleton<EquipManager>
{
    [Header("EquipPlace")]
    public GameObject WeaponPlace;
    public GameObject HelmePlacet;
    public GameObject TopPlace;
    public GameObject ArmorPlace;
    //public GameObject BottomPlace;
    public GameObject CapePlace;
    public GameObject ShieldPlace;

    [Header("Equiped")]
    public GameObject EquipedWeapon;
    public GameObject EquipedHelmet;
    public GameObject EquipedTop;
    public GameObject EquipedArmor;
    public GameObject EquipedBottom;
    public GameObject EquipedCape;
    public GameObject EquipedShield;


    private SpriteRenderer _weaponSpriteRenderer; // WeaponObject�� SpriteRenderer
    void Awake()
    {
        // WeaponObject�� SpriteRenderer ������Ʈ�� ��������
        _weaponSpriteRenderer = WeaponPlace.GetComponentInChildren<SpriteRenderer>();

        // ������ ���Ⱑ ���� ���, ��������Ʈ�� null�� ����
        if (WeaponPlace == null)
        {
            SetWeaponSprite(null); // ��������Ʈ�� null�� �����Ͽ� "�Ǽ�" ���·� ����
        }
        else
        {
            SetWeaponSprite(WeaponPlace); // ������ ������ ��������Ʈ�� ����
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
            SpriteRenderer weaponSpriteRenderer = weaponPrefab.GetComponentInChildren<SpriteRenderer>();
            _weaponSpriteRenderer.sprite = weaponSpriteRenderer.sprite; // WeaponObject�� ��������Ʈ ����
        }
    }
}
