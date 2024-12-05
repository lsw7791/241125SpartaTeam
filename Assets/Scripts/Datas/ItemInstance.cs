using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public int id; //서버에서 발급해주는 아이템
    public int itemid; //기획 데이터에 있는 아이템 아이디
    public ItemType itemtype;
    public string itemname;
    public string itemdesc;
    public int itemtier;
    public int itemhealth;
    public int itemstamina;
    public int itemdefense;
    public int itemattackp;
    public int itemattackm;
    public int itemattackmining;
    public int itemsell;
    public int itembuy;
    public float itemSpeed;
    public float itemdrop;

    public int count; // how many Items
    public int enhance; // 강화등급

}
