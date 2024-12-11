using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{ // �����۰� ���õ� ������ ����
    public int id;         // �������� �߱��ϴ� ���� ������ ID
    public int itemId;     // ��ȹ �����Ϳ��� ���ǵ� ������ ID
    public ItemType type;  // ������ Ÿ�� (������: Sword, Bow ��)
    public string name;    // ������ �̸�
    public string desc;    // ������ ����
    public int tier;       // ������ ���
    public int health;     // �߰� ü�� ����
    public int stamina;    // �߰� ���¹̳� ����
    public int defense;    // ����
    public int attack;     // �⺻ ���ݷ�
    public int attackM;    // ���� ���ݷ�
    public int attackMine; // ä��/Ư�� ���ݷ�
    public int sell;       // ������ �Ǹ� ����
    public int buy;        // ������ ���� ����
    public float speed;    // �̵� �ӵ� ����
    public float drop;     // �����(������ ȹ�� Ȯ��)
    public string prefabsPath; // �������� ������ ���
    public string imagePath;   // �������� �̹��� ���

}
