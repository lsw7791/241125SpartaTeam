using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    public DataManager dataManager;
    public CraftingManager craftingManager;
    public SoundManager soundManager;
    public UIManager uIManager;

    GameObject miniCamera;
    GameObject playerObject;
    GameObject SoundManagerObject;
    public Player player;
    protected override void Awake()
    {
        base.Awake();
        dataManager = new DataManager();
        craftingManager = new CraftingManager();
        List<ItemInstance> items = new List<ItemInstance>(); // JSON파일에 저장된 유저 아이템 정보를 읽어서 넘겨준다.
        ItemManager.Instance.Initialize(items);

    }
    private void Start()
    {
        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer_Backup"));
        player = playerObject.GetComponent<Player>();
        miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
        SpawnManager.Instance.Initialize();
        uIManager = new UIManager();
        soundManager = GetComponentInChildren<SoundManager>();
    }
}