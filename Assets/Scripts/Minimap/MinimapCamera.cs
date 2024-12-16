using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Transform player;  // �÷��̾��� Transform
    private float zFreez = -10f; // �̴ϸ� ī�޶��� ����
    private Camera minimapCamera;  // �̴ϸ� ī�޶�
    private RenderTexture minimapTexture;
    public float minSize = 0f; // �ּ� �̴ϸ� ������
    public float maxSize = 30f; // �ִ� �̴ϸ� ������
    public float sizeChangeAmount = 1f; // ũ�� ������
    private void Awake()
    {    
        minimapCamera = GetComponent<Camera>();
    }
    private void Start()
    {
        // "Player" �±װ� ���� ù ��° ������Ʈ ã��
        player = GameManager.Instance.SpawnManager.playerObject.transform;

        if (player == null)
        {
            Debug.LogError("Player ������Ʈ�� ã�� �� �����ϴ�. 'Player' �±װ� ���� ������Ʈ�� �ʿ��մϴ�.");
        }
        minimapTexture = Resources.Load<RenderTexture>("Prefabs/Cameras/MinimapRenderTexture");

        if (minimapTexture == null)
        {
            Debug.LogError("RenderTexture�� �ҷ��� �� �����ϴ�. 'MinimapTexture' �̸��� RenderTexture�� Resources ������ �߰��ؾ� �մϴ�.");
        }

        // �̴ϸ� ī�޶� RenderTexture �Ҵ�
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
        // ī�޶�� �÷��̾ ���󰡵�, Y ��ġ�� ���� (ž�ٿ�)
        minimapCamera.transform.position = new Vector3(player.position.x, player.position.y, zFreez);

        // `9`�� Ű�� �̴ϸ� ī�޶� ������ ����
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // ī�޶� ����� ������Ű��, �ִ밪�� ���� �ʵ��� ��
            minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize + sizeChangeAmount, maxSize);
        }

        // `0`�� Ű�� �̴ϸ� ī�޶� ������ ����
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // ī�޶� ����� ���ҽ�Ű��, �ּҰ��� ���� �ʵ��� ��
            minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - sizeChangeAmount, minSize);
        }
    }
}