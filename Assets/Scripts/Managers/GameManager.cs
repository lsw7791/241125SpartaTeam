using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    //public ProjectilePool projectilePool; // 발사체 풀
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON파일에 저장된 유저 아이템 정보를 읽어서 넘겨준다.
       
        ItemManager.Instance.Initialize(items);
        SpawnManager.Instance.Initialize();
    }

}