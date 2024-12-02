using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // UI 상태 변수는 이제 UIManager에서 직접 참조하므로 필요 없음
    [SerializeField] GameObject inventoryUI; // 인벤토리 UI
    [SerializeField] GameObject mapUI;       // 맵 UI
    [SerializeField] GameObject questUI;     // 퀘스트 UI
    [SerializeField] GameObject optionUI;    // 옵션 UI
    [SerializeField] GameObject statusUI;    // 스태터스 UI

    private PlayerAnimationController _playerAnimationController;

    private void Awake()
    {
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();

        // UIManager의 변수들은 싱글톤을 통해 바로 접근 가능
        inventoryUI = UIManager.Instance.inventoryUI;
        mapUI = UIManager.Instance.mapUI;
        questUI = UIManager.Instance.questUI;
        optionUI = UIManager.Instance.optionUI;
        statusUI = UIManager.Instance.statusUI;

        // 초기에는 UI 비활성화
        inventoryUI.SetActive(false);
        mapUI.SetActive(false);
        questUI.SetActive(false);
        optionUI.SetActive(false);
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
        bool isInventoryOpen = !UIManager.Instance.isInventoryOpen;
        UIManager.Instance.inventoryUI.SetActive(isInventoryOpen);
        UIManager.Instance.isInventoryOpen = isInventoryOpen;
    }

    // 맵 토글
    private void ToggleMap()
    {
        bool isMapOpen = !UIManager.Instance.isMapOpen;
        UIManager.Instance.mapUI.SetActive(isMapOpen);
        UIManager.Instance.isMapOpen = isMapOpen;
    }

    // 퀘스트 토글
    private void ToggleQuest()
    {
        bool isQuestOpen = !UIManager.Instance.isQuestOpen;
        UIManager.Instance.questUI.SetActive(isQuestOpen);
        UIManager.Instance.isQuestOpen = isQuestOpen;
    }

    // 옵션 토글
    private void ToggleOption()
    {
        if (UIManager.Instance.isInventoryOpen || UIManager.Instance.isMapOpen || UIManager.Instance.isQuestOpen || UIManager.Instance.isStatusOpen)
        {
            UIManager.Instance.CloseAllUIs(); // 모든 UI 닫기
        }
        else
        {
            bool isOptionOpen = !UIManager.Instance.isOptionOpen;
            UIManager.Instance.optionUI.SetActive(isOptionOpen);
            UIManager.Instance.isOptionOpen = isOptionOpen;
        }
    }

    // 스태터스 토글
    private void ToggleStatus()
    {
        bool isStatusOpen = !UIManager.Instance.isStatusOpen;
        UIManager.Instance.statusUI.SetActive(isStatusOpen);
        UIManager.Instance.isStatusOpen = isStatusOpen;
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
