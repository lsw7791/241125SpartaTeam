using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    GameObject miniCameraPrefab;
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON파일에 저장된 유저 아이템 정보를 읽어서 넘겨준다.
       
        ItemManager.Instance.Initialize(items);
        SpawnManager.Instance.Initialize();
        miniCameraPrefab = Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera");
    }
    private void Start()
    {
        GameObject miniCamera = Instantiate(miniCameraPrefab);
    }
}