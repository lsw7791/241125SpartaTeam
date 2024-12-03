using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI; // 인벤토리 UI
    [SerializeField] GameObject mapUI;       // 맵 UI
    [SerializeField] GameObject questUI;     // 퀘스트 UI
    [SerializeField] GameObject optionUI;    // 옵션 UI
    [SerializeField] GameObject statusUI;    // 스태터스 UI

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
        if (!UIManager.Instance.IsExistUI<InventoryUI>())
        {
            inventoryUI = UIManager.Instance.OpenUI<InventoryUI>().gameObject;
            return;
        }
        else
        {
            bool isInventoryOpen = !inventoryUI.activeSelf;

            inventoryUI.SetActive(isInventoryOpen);
        }
    }

    // 맵 토글
    private void ToggleMap()
    {
        if (!UIManager.Instance.IsExistUI<MapUI>())
        {
            mapUI = UIManager.Instance.OpenUI<MapUI>().gameObject;
            return;
        }
        else
        {
            bool isMapOpen = !mapUI.activeSelf;

            mapUI.SetActive(isMapOpen);
        }
    }

    // 퀘스트 토글
    private void ToggleQuest()
    {
        if (!UIManager.Instance.IsExistUI<QuestUI>())
        {
            questUI = UIManager.Instance.OpenUI<QuestUI>().gameObject;
            return;
        }
        else
        {
            bool isQuestOpen = !questUI.activeSelf;

            questUI.SetActive(isQuestOpen);
        }
    }

    // 옵션 토글
    private void ToggleOption()
    {
        if (inventoryUI.activeSelf || mapUI.activeSelf || questUI.activeSelf || statusUI.activeSelf)
        {
            UIManager.Instance.CloseAllUIs(); // 모든 UI 닫기
        }
        else
        {
            if (!UIManager.Instance.IsExistUI<OptionUI>())
            {
                optionUI = UIManager.Instance.OpenUI<OptionUI>().gameObject;
                return;
            }
            else
            {
                bool isOptionOpen = !optionUI.activeSelf;

                optionUI.SetActive(isOptionOpen);
            }
        }
    }

    // 스태터스 토글
    private void ToggleStatus()
    {
        if (!UIManager.Instance.IsExistUI<StatusUI>())
        {
            statusUI = UIManager.Instance.OpenUI<StatusUI>().gameObject;
            return;
        }
        else
        {
            bool isStatusOpen = !statusUI.activeSelf;

            statusUI.SetActive(isStatusOpen);
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
