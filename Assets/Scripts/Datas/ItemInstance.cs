using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{ // 아이템과 관련된 정보를 관리
    public int id;         // 서버에서 발급하는 고유 아이템 ID
    public int itemId;     // 기획 데이터에서 정의된 아이템 ID
    public ItemType type;  // 아이템 타입 (열거형: Sword, Bow 등)
    public string name;    // 아이템 이름
    public string desc;    // 아이템 설명
    public int tier;       // 아이템 등급
    public int health;     // 추가 체력 제공
    public int stamina;    // 추가 스태미나 제공
    public int defense;    // 방어력
    public int attack;     // 기본 공격력
    public int attackM;    // 마법 공격력
    public int attackMine; // 채광/특수 공격력
    public int sell;       // 아이템 판매 가격
    public int buy;        // 아이템 구매 가격
    public float speed;    // 이동 속도 증가
    public float drop;     // 드랍율(아이템 획득 확률)
    public string prefabsPath; // 아이템의 프리팹 경로
    public string imagePath;   // 아이템의 이미지 경로

}
