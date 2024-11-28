using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrent : MonoBehaviour
{
    [SerializeField] int _currentHP;                        // ü��
    [SerializeField] int _currentStamina;                   // ���¹̳�
    [SerializeField] int _currentGold;                      // ���
    [SerializeField] int _currentDamage;                    // ���ݷ�
    [SerializeField] int _currentSpeed;                     // �̵� �ӵ�
    [SerializeField] float _currentATKSpeed;             // ���� �ӵ�
    [SerializeField] int _currentDef;                       // ����

    [SerializeField] string NickName;               // �÷��̾� �г���

    [SerializeField] float _movingLV;
    [SerializeField] float _makingLV;
    [SerializeField] float _minigWPLV;
    [SerializeField] float _nearWPLV;
    [SerializeField] float _arrowWPLV;
    [SerializeField] float _staffWPLV;

    public Inventory PlayerInventory;     // �κ��丮
    public List<QuickSlotItem> QuickSlots; // ������ ������ ���
}
