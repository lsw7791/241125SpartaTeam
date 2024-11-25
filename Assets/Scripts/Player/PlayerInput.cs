using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private bool isInventoryOpen = false;  // 인벤토리 상태를 추적하는 변수
    private bool isMapOpen = false;        // 맵 상태 추적
    private bool isQuestOpen = false;      // 퀘스트 상태 추적
    private bool isOptionsOpen = false;    // 옵션 상태 추적

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
        if (context.performed)
        {
            PerformPadding();
        }
    }

    // 인벤토리
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleInventory();
        }
    }

    // 맵
    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleMap();
        }
    }

    // 퀘스트
    public void OnQuest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleQuest();
        }
    }

    // 옵션
    public void OnOption(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleOptions();
        }
    }

    // 인벤토리 토글
    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            // inventoryUI.SetActive(true); 
        }
        else
        {
            // inventoryUI.SetActive(false);  
        }
    }

    // 맵 토글
    private void ToggleMap()
    {
        isMapOpen = !isMapOpen;

        if (isMapOpen)
        {
            // mapUI.SetActive(true);
        }
        else
        {
            // mapUI.SetActive(false);
        }
    }

    // 퀘스트 토글
    private void ToggleQuest()
    {
        isQuestOpen = !isQuestOpen;

        if (isQuestOpen)
        {
            // questUI.SetActive(true);
        }
        else
        {
            // questUI.SetActive(false);
        }
    }

    // 옵션 토글
    private void ToggleOptions()
    {
        isOptionsOpen = !isOptionsOpen;

        if (isOptionsOpen)
        {
            // optionsUI.SetActive(true);
        }
        else
        {
            // optionsUI.SetActive(false);
        }
    }

    // 채집 로직
    private void PerformAction()
    {
        // 채집 로직 추가
    }

    // 공격 로직
    private void PerformAttack()
    {
        // 공격 로직 추가
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
    private void PerformPadding()
    {
        // 막기 로직 추가
    }
}
