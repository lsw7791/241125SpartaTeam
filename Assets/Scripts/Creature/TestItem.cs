using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // ������ ������ ����
    //public Sprite itemSprite; // �������� ��������Ʈ (�̹���)
    private int minGold; // �ּ� ���
    private int maxGold; // �ִ� ���
    public bool isPlayerDrop = false;

    private int _maxBounce = 10;

    private float _xForce = 5;
    private float _yForce = 10;
    private float _gravity = 0.1f;

    private Vector2 _direction;
    private int _curBounce = 0;
    private bool _isGrouned = true;
    
    private float _maxHeight;
    private float _curHeight;

    [SerializeField] private Transform ItemImage;

    private void Start()
    {
        if(isPlayerDrop)
        {
            Invoke("PlayerDropItem", 5);
        }
    }

    private void OnEnable()
    {
        _curBounce = 0;

        _curHeight = Random.Range(_yForce - 1, _yForce);
        _maxHeight = _curHeight;
        Bounce(new Vector2(Random.Range(-_xForce,_xForce),Random.Range(-_xForce,_xForce)));
    }

    private void Update()
    {
        if (!_isGrouned)
        {
            _curHeight -= _gravity * Time.deltaTime;

            if (ItemImage != null)
            {
                ItemImage.position += new Vector3(0, _curHeight, 0) * Time.deltaTime;
            }
            transform.position += (Vector3)_direction * Time.deltaTime;

            BounceHit();
        }
    }

    private void Bounce(Vector2 inDirection)
    {
        _isGrouned = false;
        _maxHeight /= 1.5f;
        _direction = inDirection;
        _curHeight = _maxHeight;
        _curBounce++;
    }

    private void BounceHit()
    {
        if(_curBounce < _maxBounce)
        {
            Bounce(_direction / 1.5f);
        }
        else
        {
            _isGrouned = true;
        }
    }

    private void PlayerDropItem()
    {
        isPlayerDrop = false;
    }

    // ������ �����͸� �����ϴ� �޼��� (����)
    public void SetData(ItemData data, int minGold, int maxGold)
    {
        itemData = data;
        this.minGold = minGold;
        this.maxGold = maxGold;
    }

    // ������ �����͸� �����ϴ� �޼��� (�Ϲ� �����ۿ�)
    public void SetData(ItemData data)
    {
        itemData = data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerDrop)
        {
            SoundManager.Instance.PlayItemPickUpSFX();
            if (itemData.itemType == ItemType.Gold) // ��� ���������� Ȯ��
            {
                int randomGold = Random.Range(minGold, maxGold + 1); // ��� �������� ���� ��
                GameManager.Instance.Player.Stats.Gold += randomGold; // �÷��̾� ��� �߰�
              
            }
            else
            {
                // �Ϲ� �������� �κ��丮�� �߰�
                GameManager.Instance.Player.AddItemToInventory(
                    itemData.id,
                    1,
                    itemData.atlasPath
                );
                if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(7) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[7])
                {
                    // QuestCompletionStatus[1]�� false�� ���� CompleteQuest(1) ȣ��
                    GameManager.Instance.DataManager.MainQuest.CompleteQuest(7);
                }
            }

            Destroy(gameObject); // ������ ����
        }
    }
}
