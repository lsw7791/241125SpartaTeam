using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoSingleton<EquipManager>
{
    public GameObject weaponObject;
    public GameObject equipedWeapon;
    //public GameObject equipedHelmet;
    //public GameObject equipedTop;
    //public GameObject equipedArmor;
    //public GameObject equipedBottom;
    //public GameObject equipedCape;
    //public GameObject equipedShield;

    //[SerializeField] private int _rWeaponID;
    //[SerializeField] private int _lWeaponID;
    //[SerializeField] private int _topID;
    //[SerializeField] private int _bottomID;
    //[SerializeField] private int _helmetID;
    //[SerializeField] private int _armorID;
    //[SerializeField] private int _capeID;


    private SpriteRenderer _weaponSpriteRenderer; // WeaponObject�� SpriteRenderer
    protected override void Awake()
    {
        // WeaponObject�� SpriteRenderer ������Ʈ�� ��������
        _weaponSpriteRenderer = weaponObject.GetComponentInChildren<SpriteRenderer>();

        // ������ ���Ⱑ ���� ���, ��������Ʈ�� null�� ����
        if (equipedWeapon == null)
        {
            SetWeaponSprite(null); // ��������Ʈ�� null�� �����Ͽ� "�Ǽ�" ���·� ����
        }
        else
        {
            SetWeaponSprite(equipedWeapon); // ������ ������ ��������Ʈ�� ����
        }
    }

    // ���ο� ���� ����
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        equipedWeapon = newWeaponPrefab; // �� ���⸦ ����

        // ���ο� ������ ��������Ʈ�� ����
        SetWeaponSprite(equipedWeapon);
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
