using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    GameObject miniCamera;
    GameObject playerObject;
    public Player player;
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON���Ͽ� ����� ���� ������ ������ �о �Ѱ��ش�.
       
        ItemManager.Instance.Initialize(items);
        SpawnManager.Instance.Initialize();
    }
    private void Start()
    {
        miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer_Backup"));
        player = playerObject.GetComponent<Player>();

    }
}