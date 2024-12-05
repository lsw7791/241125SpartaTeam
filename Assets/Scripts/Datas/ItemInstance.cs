using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public int id; //서버에서 발급해주는 아이템
    public int itemId; //기획 데이터에 있는 아이템 아이디
    public ItemType type;
    public string name;
    public string desc;
    public int tier;
    public int health;
    public int stamina;
    public int defense;
    public int attack;
    public int attackM;
    public int attackMine;
    public int sell;
    public int buy;
    public float speed;
    public float drop;
    public string prefabsPath;
    public string imagePath;

}
