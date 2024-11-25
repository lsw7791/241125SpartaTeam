using UnityEngine;
using UnityEngine.InputSystem;

public class QuickSlotsController : MonoBehaviour
{
    // 퀵슬롯 인덱스 변수 (1~8번 슬롯)
    private int currentQuickSlot = 0;  // 현재 선택된 슬롯
    private string[] quickSlots = new string[8];  // 각 퀵슬롯에 해당하는 아이템/스킬 저장

    // 키보드 숫자 키 1~8번에 대응하는 입력을 받을 수 있도록 설정
    private void OnEnable()
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            quickSlots[i] = "Item" + (i + 1);  // 기본 값 설정 
        }
    }

    // 키보드 입력으로 퀵슬롯 선택 (1~8)
    public void OnQuickSlot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int keyNumber = (int)context.ReadValue<float>();
            if (keyNumber >= 1 && keyNumber <= 8)
            {
                currentQuickSlot = keyNumber - 1;  // 1~8을 0~7로 매핑
                SelectQuickSlot(currentQuickSlot);
            }
        }
    }

    // 마우스 휠로 퀵슬롯 스왑 (위로 올리면 +1, 아래로 내리면 -1)
    public void OnSwap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 scrollValue = context.ReadValue<Vector2>();
            if (scrollValue.y > 0)  // 휠 위로 스크롤
            {
                NextSlot();
            }
            else if (scrollValue.y < 0)  // 휠 아래로 스크롤
            {
                PreviousSlot();
            }
        }
    }

    // 퀵슬롯 선택 (선택된 슬롯의 아이템 출력)
    private void SelectQuickSlot(int slotIndex)
    {
        string selectedItem = quickSlots[slotIndex];
        Debug.Log("Selected Quick Slot: " + selectedItem);
    }

    // 퀵슬롯을 다음으로 순환
    private void NextSlot()
    {
        currentQuickSlot = (currentQuickSlot + 1) % quickSlots.Length;  // 슬롯 순환
        SelectQuickSlot(currentQuickSlot);
    }

    // 퀵슬롯을 이전으로 순환
    private void PreviousSlot()
    {
        currentQuickSlot = (currentQuickSlot - 1 + quickSlots.Length) % quickSlots.Length;  // 슬롯 순환
        SelectQuickSlot(currentQuickSlot);
    }
}
