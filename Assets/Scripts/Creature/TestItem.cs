using UnityEngine;
using MainData;

public class TestItem : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터 저장
    //public Sprite itemSprite; // 아이템의 스프라이트 (이미지)
    private int minGold; // 최소 골드
    private int maxGold; // 최대 골드
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

    // 아이템 데이터를 설정하는 메서드 (골드용)
    public void SetData(ItemData data, int minGold, int maxGold)
    {
        itemData = data;
        this.minGold = minGold;
        this.maxGold = maxGold;
    }

    // 아이템 데이터를 설정하는 메서드 (일반 아이템용)
    public void SetData(ItemData data)
    {
        itemData = data;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerDrop)
        {
            SoundManager.Instance.PlayItemPickUpSFX();
            if (itemData.itemType == ItemType.Gold) // 골드 아이템인지 확인
            {
                int randomGold = Random.Range(minGold, maxGold + 1); // 골드 범위에서 랜덤 값
                GameManager.Instance.Player.Stats.Gold += randomGold; // 플레이어 골드 추가
              
            }
            else
            {
                // 일반 아이템은 인벤토리에 추가
                GameManager.Instance.Player.AddItemToInventory(
                    itemData.id,
                    1,
                    itemData.atlasPath
                );
                if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(7) &&
           !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[7])
                {
                    // QuestCompletionStatus[1]이 false일 때만 CompleteQuest(1) 호출
                    GameManager.Instance.DataManager.MainQuest.CompleteQuest(7);
                }
            }

            Destroy(gameObject); // 아이템 제거
        }
    }
}
