using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    public DataManager dataManager;
    public CraftingManager craftingManager;
    public SoundManager soundManager;
    public UIManager uIManager;
    public SpawnManager spawnManager;
    GameObject SoundManagerObject;
    public Player player;
    public int sceneNum;
    public IInteractable InteractableObject { get; set; } // ���� ��ȣ�ۿ� ������ ��ü
    protected override void Awake()
    {
        base.Awake();
        dataManager = new DataManager();
        if (craftingManager == null) craftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>(); // JSON���Ͽ� ����� ���� ������ ������ �о �Ѱ��ش�.
        ItemManager.Instance.Initialize(items);
        sceneNum = 2;
    }
    private void Start()
    {      
        SceneManager.sceneLoaded += OnSceneLoaded;
        Instantiate(sceneNum);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {      
        if (scene.name == dataManager.scene.GetMapTo(2))// TO Forest
        {
            Instantiate(sceneNum);
        }
        else if (scene.name == dataManager.scene.GetMapTo(1))//To ����
        {
            Instantiate(sceneNum);
        }
    }
    void Instantiate(int mapNum)
    {
        GameObject miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
        if(spawnManager==null)spawnManager = gameObject.AddComponent<SpawnManager>();
        if(uIManager ==null) uIManager = gameObject.AddComponent<UIManager>();
        if (soundManager == null) soundManager = gameObject.AddComponent<SoundManager>();
        GameManager.Instance.spawnManager.SpawnPlayer(mapNum);
        spawnManager.Initialize(mapNum);
    }
    public void LoadScene(string sceneName)
    {
        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
    //GameObject ManagerSpawn(string inManagerName)
    //{
    //    GameObject outManager = new GameObject();
    //    outManager.name = $"{inManagerName} Manager";
    //    return outManager;
    //}
}