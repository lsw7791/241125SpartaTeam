using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public GameObject WeaponPlace;
    public GameObject EquipedWeapon;
    public GameObject EquipedHelmet;
    public GameObject EquipedTop;
    public GameObject EquipedArmor;
    //public GameObject equipedBottom;
    public GameObject EquipedCape;
    public GameObject EquipedShield;

    //[SerializeField] private int _rWeaponID;
    //[SerializeField] private int _lWeaponID;
    //[SerializeField] private int _topID;
    //[SerializeField] private int _bottomID;
    //[SerializeField] private int _helmetID;
    //[SerializeField] private int _armorID;
    //[SerializeField] private int _capeID;


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
