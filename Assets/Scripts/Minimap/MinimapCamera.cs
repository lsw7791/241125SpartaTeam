using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Transform player;  // 플레이어의 Transform
    private float zFreez = -10f; // 미니맵 카메라의 높이
    private Camera minimapCamera;  // 미니맵 카메라
    private RenderTexture minimapTexture;
    public float minSize = 0f; // 최소 미니맵 사이즈
    public float maxSize = 30f; // 최대 미니맵 사이즈
    public float sizeChangeAmount = 1f; // 크기 조정량
    private void Awake()
    {    
        minimapCamera = GetComponent<Camera>();
    }
    private void Start()
    {
        // "Player" 태그가 붙은 첫 번째 오브젝트 찾기
        player = GameManager.Instance.SpawnManager.playerObject.transform;

        if (player == null)
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 'Player' 태그가 붙은 오브젝트가 필요합니다.");
        }
        minimapTexture = Resources.Load<RenderTexture>("Prefabs/Cameras/MinimapRenderTexture");

        if (minimapTexture == null)
        {
            Debug.LogError("RenderTexture를 불러올 수 없습니다. 'MinimapTexture' 이름의 RenderTexture를 Resources 폴더에 추가해야 합니다.");
        }

        // 미니맵 카메라에 RenderTexture 할당
        if (minimapCamera != null && minimapTexture != null)
        {
            minimapCamera.targetTexture = minimapTexture;
        }
    }
    private void Update()
    {
        if(player==null)
        {
            player = GameManager.Instance.SpawnManager.playerObject.transform;
        }
        // 카메라는 플레이어를 따라가되, Y 위치는 고정 (탑다운)
        minimapCamera.transform.position = new Vector3(player.position.x, player.position.y, zFreez);

        // `9`번 키로 미니맵 카메라 사이즈 증가
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // 카메라 사이즈를 증가시키되, 최대값을 넘지 않도록 함
            minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize + sizeChangeAmount, maxSize);
        }

        // `0`번 키로 미니맵 카메라 사이즈 감소
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // 카메라 사이즈를 감소시키되, 최소값을 넘지 않도록 함
            minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - sizeChangeAmount, minSize);
        }
    }
}