using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Camera _camera;
    private PlayerMove playerMove;
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    public event Action<QuestAction> OnQuestActionTriggered;
    bool playerPadding = false;
    private Padding padding;
    private void Awake()
    {
        _camera = Camera.main;
        playerMove = GetComponent<PlayerMove>();
        padding = GetComponentInChildren<Padding>();
        padding.gameObject.SetActive(false);
    }
    //상호작용
    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.playerState == Player.PlayerState.Die) return;
        //if (GameManager.Instance.Player.playerState == Player.PlayerState.UIOpen) return;

        playerMove.moveInput = context.ReadValue<Vector2>();
        bool isMoving = playerMove.moveInput.sqrMagnitude > 0; // 벡터 크기로 이동 여부 판단

        // Dictionary에서 QuestID 1에 해당하는 값 확인
        if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(0) &&
            !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[0])
        {
            // QuestCompletionStatus[1]이 false일 때만 CompleteQuest(1) 호출
            GameManager.Instance.DataManager.MainQuest.CompleteQuest(0);
        }

        GameManager.Instance.Player._playerAnimationController.SetMoveAnimation(isMoving);
    }




    // 마우스 위치에 따른 회전 처리
    public void OnLook(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.playerState == Player.PlayerState.Die) return;
        if (GameManager.Instance.Player.playerState == Player.PlayerState.UIOpen) return;
        if (context.performed)
        {
            if (_camera == null)
                _camera = Camera.main;
            if (_camera != null)
            {
                Vector2 mouseScreenPos = context.ReadValue<Vector2>(); // 마우스 화면 좌표
                Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(mouseScreenPos); // 월드 좌표로 변환
                Vector2 direction = (Vector2)transform.position - mouseWorldPos;
                // 플레이어와 마우스 위치를 비교하여 좌우 반전
                GameManager.Instance.Player._playerAnimationController.FlipRotation(mouseWorldPos);
                RotateArm(direction);
            }
        }
    }
    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Interact();
        }
    }

    // 채집
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PerformAction();
        }
    }

    // 공격
    public void OnAttack(InputAction.CallbackContext context)
    {
        // UI가 활성화된 상태인지 확인
        //if (UIManager.Instance.IsActiveUI())
        //{
        //    return;
        //}
        //Debug.Log(GameManager.Instance.Player.playerState);
        if (GameManager.Instance.Player.playerState == Player.PlayerState.UIOpen) return;
        if (GameManager.Instance.Player.playerState == Player.PlayerState.Die) return;

        GameManager.Instance.Player._playerAnimationController.TriggerAttackAnimation();
    }


    // 구르기
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameManager.Instance.Player.UseStamina(20) == true)
            {
                SoundManager.Instance.PlayRollSFX();
                PerformRoll();
            }
        }
    }

    // 패딩
    public void OnPadding(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameManager.Instance.Player.UseStamina(15) == true)
            {
                GameManager.Instance.Player._playerAnimationController.SetPaddingAnimation(true); // 애니메이션 활성화
            PerformPaddingStart(); // 패딩 동작 시작
            padding.gameObject.SetActive(true);
                SoundManager.Instance.PlayDefenseSFX();
                //padding.InsertSprite();
            }
        }
        else if (context.canceled)
        {
            GameManager.Instance.Player._playerAnimationController.SetPaddingAnimation(false); // 애니메이션 비활성화
            PerformPaddingEnd(); // 패딩 동작 종료
            padding.gameObject.SetActive(false);
        }
    }

    // 인벤토리 토글
    public void OnQuest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.PlayBookSFX();
            UIManager.Instance.ToggleUI<MainQuestUI>();
        }
    } 
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {        
        SoundManager.Instance.PlayBookSFX();
            ToggleInventory();
        }
    }

    // 맵 토글
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        SoundManager.Instance.PlayBookSFX();
            ToggleMap();
        }
    }

    public void OnCraft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.PlayBookSFX();

            // UI 토글
            UIManager.Instance.ToggleUI<CraftUI>();
           
        }
    }


    // 옵션 토글
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        SoundManager.Instance.PlayBookSFX();
            ToggleOption();
        }
    }

    // 스태터스 토글
    public void OnStatus(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        SoundManager.Instance.PlayBookSFX();
            UIManager.Instance.ToggleUI<StatusUI>();

            if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(4) &&
               !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[4])
            {
                GameManager.Instance.DataManager.MainQuest.CompleteQuest(4);
            }
        }
    }
    public void OnInfo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SoundManager.Instance.PlayBookSFX();
            ToggleInfo();

            if (GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus.ContainsKey(1) &&
                !GameManager.Instance.DataManager.MainQuest.QuestCompletionStatus[1])
            {
                // QuestCompletionStatus[1]이 false일 때만 CompleteQuest(1) 호출
                GameManager.Instance.DataManager.MainQuest.CompleteQuest(1);
            }
        }
    }
    // 인벤토리 토글
    private void ToggleInventory()
    {
        UIManager.Instance.ToggleUI<InventoryUI>();
    }

    // 맵 토글
    private void ToggleMap()
    {
        UIManager.Instance.ToggleUI<CheatUI>();
    }

    // 퀘스트 토글


    // 옵션 토글
    private void ToggleOption()
    {
        // 활성화된 UI가 있으면 모든 UI를 닫는다.
        if (UIManager.Instance.IsActiveUI())
        {
            UIManager.Instance.CloseAllUIs(); // 모든 UI를 닫음
            //UIManager.Instance.ToggleUI<MainQuestUI>();
        }
        else
        {
            // 활성화된 UI가 없으면 OptionUI를 토글
            UIManager.Instance.ToggleUI<OptionUI>();
        }
    }
    private void ToggleInfo()
    {
            UIManager.Instance.ToggleUI<InfoUI>();
    }





    // 채집 로직
    private void PerformAction()
    {
        // 채집 로직 추가
    }

    // 공격 로직
    private void PerformAttack()
    {
        // PlayerWeapon에 있는 ActivateWeaponCollider()를 호출하여 콜라이더 활성화
      //  PlayerWeapon playerWeapon = EquipManager.Instance.WeaponObject.GetComponent<PlayerWeapon>();

    }

    // 상호작용 로직
    private void Interact()
    {
        if(GameManager.Instance.InteractableObject == null)
        {
            return;
        }
        GameManager.Instance.InteractableObject.Interact();
    }

    // 구르기 로직
    private void PerformRoll()
    {
        Debug.Log("PerformRoll");

        // 이미 구르고 있으면 구르기 시작하지 않음
        if (GameManager.Instance.Player.playerRoll.isRolling) return;
            // 구르기 시작
            GameManager.Instance.Player.playerRoll.StartRolling();
        // 구르기 시작 후, Roll은 Update에서 진행됩니다.
        // Update에서 계속해서 Roll()을 호출하게 됩니다.
    }

    // 패딩(막기) 로직
    private void PerformPaddingStart()
    {
            playerPadding = true;
            // 막기 동작 시작 시 실행할 로직
            GameManager.Instance.Player.IncreaseDefense(2);
            Debug.Log("Padding started!");
    }

    private void PerformPaddingEnd()
    {
        // 막기 동작 종료 시 실행할 로직
        int value = GameManager.Instance.Player.stats.Def;
        GameManager.Instance.Player.ResetDefense();
        Debug.Log("Padding ended!");
        playerPadding=false;
    }
    public void RotateArm(Vector2 direction)
    {
        // 팔의 회전 계산
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);  // 회전 적용
    }
}
