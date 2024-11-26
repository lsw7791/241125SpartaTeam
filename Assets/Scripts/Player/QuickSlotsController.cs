using UnityEngine;
using UnityEngine.InputSystem;

public class QuickSlotsController : MonoBehaviour
{
    [SerializeField] private int currentQuickSlot = 0;  // 현재 선택된 슬롯
    [SerializeField] private string[] quickSlots = new string[8];  // 슬롯 데이터

    private void OnEnable()
    {
        // 기본 슬롯 데이터를 설정
        for (int i = 0; i < quickSlots.Length; i++)
        {
            quickSlots[i] = "Item" + (i + 1);
        }
    }

    // 키보드 입력 처리
    public void OnQuickSlot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 바인딩된 숫자를 읽어오기 위한 방법
            string bindingName = context.control.displayName;

            // 숫자 키 바인딩에 해당하는 값을 추출
            int keyNumber = bindingName[0] - '0';  // '1' -> 1, '2' -> 2 등
            if (keyNumber >= 1 && keyNumber <= 8)
            {
                currentQuickSlot = keyNumber - 1;  // 1~8을 0~7로 매핑
                SelectQuickSlot(currentQuickSlot);
                NotifySlotChange();  // UI 업데이트 호출
            }
        }
    }

    // 퀵슬롯 선택
    private void SelectQuickSlot(int slotIndex)
    {
        // 선택된 슬롯을 처리
        string selectedItem = quickSlots[slotIndex];
    }

    // 마우스 휠 처리
    public void OnSwap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 scrollValue = context.ReadValue<Vector2>();
            if (scrollValue.y > 0)
                NextSlot();
            else if (scrollValue.y < 0)
                PreviousSlot();
        }
    }

    // 슬롯 변경 처리
    private void NextSlot()
    {
        currentQuickSlot = (currentQuickSlot + 1) % quickSlots.Length;
        NotifySlotChange();
    }

    private void PreviousSlot()
    {
        currentQuickSlot = (currentQuickSlot - 1 + quickSlots.Length) % quickSlots.Length;
        NotifySlotChange();
    }

    // 이벤트 호출
    private void NotifySlotChange()
    {
        EventBroker.NotifyQuickSlotChanged(currentQuickSlot);  // UI에 슬롯 변경 알림
    }
}
