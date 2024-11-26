using System;
using UnityEngine;

public class EventBroker : MonoBehaviour
{
    public static event Action<int> QuickSlotChanged;

    // 이벤트 호출 메서드
    public static void NotifyQuickSlotChanged(int selectedSlotIndex)
    {
        QuickSlotChanged?.Invoke(selectedSlotIndex);
    }
}
