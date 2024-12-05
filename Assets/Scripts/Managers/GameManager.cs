using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    //public ProjectilePool projectilePool; // �߻�ü Ǯ
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON���Ͽ� ����� ���� ������ ������ �о �Ѱ��ش�.
       
        ItemManager.Instance.Initialize(items);
        SpawnManager.Instance.Initialize();
    }

}