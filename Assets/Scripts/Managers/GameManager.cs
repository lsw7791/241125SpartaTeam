using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    public DataManager dataManager;
    public CraftingManager craftingManager;
    public SoundManager soundManager;
    public SpawnManager spawnManager;
    public CharacterSlotManager slotManager;
    GameObject SoundManagerObject;
    public Player player;
    public int sceneNum;
    public IInteractable InteractableObject { get; set; } // 현재 상호작용 가능한 객체
    protected override void Awake()
    {
        base.Awake();

        // 데이터 매니저와 슬롯 매니저 초기화
        dataManager = new DataManager();
        slotManager = new CharacterSlotManager();

        // 다른 매니저 초기화
        if (craftingManager == null) craftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        sceneNum = 24;
    }

    private void Start()
    {      
        SceneManager.sceneLoaded += OnSceneLoaded;
        Instantiate(sceneNum);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnManager.playerObject.SetActive(false);
        if (scene.name == dataManager.scene.GetMapTo(sceneNum))// TO Forest
        {
            Instantiate(sceneNum);
        }       
    }
    void Instantiate(int mapNum)
    {
        GameObject miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
        if(spawnManager==null)spawnManager = gameObject.AddComponent<SpawnManager>();
        if (soundManager == null) soundManager = gameObject.AddComponent<SoundManager>();
        spawnManager.SpawnPlayer(mapNum);
        spawnManager.Initialize(mapNum);
    }
    public void LoadScene(string sceneName)
    {
        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }
    //GameObject ManagerSpawn(string inManagerName)
    //{
    //    GameObject outManager = new GameObject();
    //    outManager.name = $"{inManagerName} Manager";
    //    return outManager;
    //}
}