using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrent : MonoBehaviour
{
    [SerializeField] int _currentHP;                        // 체력
    [SerializeField] int _currentStamina;                   // 스태미나
    [SerializeField] int _currentGold;                      // 골드
    [SerializeField] int _currentDamage;                    // 공격력
    [SerializeField] int _currentSpeed;                     // 이동 속도
    [SerializeField] float _currentATKSpeed;             // 공격 속도
    [SerializeField] int _currentDef;                       // 방어력

    [SerializeField] string NickName;               // 플레이어 닉네임

    [SerializeField] float _movingLV;
    [SerializeField] float _makingLV;
    [SerializeField] float _minigWPLV;
    [SerializeField] float _nearWPLV;
    [SerializeField] float _arrowWPLV;
    [SerializeField] float _staffWPLV;

    public Inventory PlayerInventory;     // 인벤토리
    public List<QuickSlotItem> QuickSlots; // 퀵슬롯 아이템 목록
}
