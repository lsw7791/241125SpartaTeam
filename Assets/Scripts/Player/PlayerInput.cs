using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    private bool isInventoryOpen = false;  // 인벤토리 상태를 추적하는 변수
    private bool isMapOpen = false;        // 맵 상태 추적
    private bool isQuestOpen = false;      // 퀘스트 상태 추적
    private bool isOptionOpen = false;    // 옵션 상태 추적
    private bool isStatusOpen = false;    // 옵션 상태 추적


    [SerializeField] GameObject inventoryUI; // 인벤토리 UI
    [SerializeField] GameObject mapUI;       // 맵 UI
    [SerializeField] GameObject questUI;     // 퀘스트 UI
    [SerializeField] GameObject optionUI;    // 옵션 UI
    [SerializeField] GameObject statusUI;    // 옵션 UI


    private void Awake()
    {
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
            ToggleOption();
        }
    }
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
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
    }

    // 맵 토글
    private void ToggleMap()
    {
        isMapOpen = !isMapOpen;
        mapUI.SetActive(isMapOpen);
    }

    // 퀘스트 토글
    private void ToggleQuest()
    {
        isQuestOpen = !isQuestOpen;
        questUI.SetActive(isQuestOpen);
    }

    // 옵션 토글
    private void ToggleOption()
    {
        // 활성화된 UI가 있는지 확인 (모든 UI 상태를 확인)
        if (isInventoryOpen || isMapOpen || isQuestOpen || isOptionOpen || isStatusOpen)
        {
            // 활성화된 UI가 있으면 모두 비활성화
            inventoryUI.SetActive(false);
            mapUI.SetActive(false);
            questUI.SetActive(false);
            optionUI.SetActive(false);
            statusUI.SetActive(false);

            // 모든 UI 상태를 비활성화
            isInventoryOpen = false;
            isMapOpen = false;
            isQuestOpen = false;
            isOptionOpen = false;
            isStatusOpen = false;
        }
        else
        {
            // 활성화된 UI가 없으면 옵션 UI만 활성화
            optionUI.SetActive(true);
            isOptionOpen = true;
        }
    }


    private void ToggleStatus()
    {
        isStatusOpen = !isStatusOpen;
        statusUI.SetActive(isStatusOpen);
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
    private void PerformPadding()
    {
        // 막기 로직 추가
    }
}