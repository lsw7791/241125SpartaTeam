using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    GameObject miniCameraPrefab;
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON���Ͽ� ����� ���� ������ ������ �о �Ѱ��ش�.
       
        ItemManager.Instance.Initialize(items);
        SpawnManager.Instance.Initialize();
        miniCameraPrefab = Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera");
    }
    private void Start()
    {
        GameObject miniCamera = Instantiate(miniCameraPrefab);
    }
}