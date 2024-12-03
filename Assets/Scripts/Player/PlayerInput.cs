using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerAnimationController _playerAnimationController;

    private void Awake()
    {
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }

    // 상호작용
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
        if (context.performed)
        {
            _playerAnimationController.TriggerAttackAnimation();
            PerformAttack();
        }
    }

    // 구르기
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PerformRoll();
        }
    }

    // 패딩
    public void OnPadding(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerAnimationController.SetPaddingAnimation(true); // 애니메이션 활성화
            PerformPaddingStart(); // 패딩 동작 시작
        }
        else if (context.canceled)
        {
            _playerAnimationController.SetPaddingAnimation(false); // 애니메이션 비활성화
            PerformPaddingEnd(); // 패딩 동작 종료
        }
    }

    // 인벤토리 토글
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleInventory();
        }
    }

    // 맵 토글
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleMap();
        }
    }

    // 퀘스트 토글
    public void OnQuest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleQuest();
        }
    }

    // 옵션 토글
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleOption();
        }
    }

    // 스태터스 토글
    public void OnStatus(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleStatus();
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
        UIManager.Instance.ToggleUI<MapUI>();
    }

    // 퀘스트 토글
    private void ToggleQuest()
    {
        UIManager.Instance.ToggleUI<QuestUI>();
    }

    // 옵션 토글
    private void ToggleOption()
    {
        if (UIManager.Instance.ActiveUI())
        {
            UIManager.Instance.CloseAllUIs(); // 모든 UI 닫기
        }
        else
        {
            UIManager.Instance.ToggleUI<OptionUI>();
        }
    }

    // 스태터스 토글
    private void ToggleStatus()
    {
        UIManager.Instance.ToggleUI<StatusUI>();
    }

    // 채집 로직
    private void PerformAction()
    {
        // 채집 로직 추가
    }

    // 공격 로직
    private void PerformAttack()
    {
    }

    // 상호작용 로직
    private void Interact()
    {
        // 상호작용 로직 추가
    }

    // 구르기 로직
    private void PerformRoll()
    {
        // 구르기 로직 추가
    }

    // 패딩(막기) 로직
    private void PerformPaddingStart()
    {
        // 막기 동작 시작 시 실행할 로직
        Debug.Log("Padding started!");
    }

    private void PerformPaddingEnd()
    {
        // 막기 동작 종료 시 실행할 로직
        Debug.Log("Padding ended!");
    }
}
