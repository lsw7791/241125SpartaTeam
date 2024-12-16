using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public PlayerData SlotData; // 슬롯의 데이터
    [SerializeField] private TextMeshProUGUI nicknameText; // 닉네임 표시
    [SerializeField] private Button selectButton; // 슬롯 선택 버튼

    // 슬롯 초기화
    public void InitializeSlot(PlayerData data)
    {
        SlotData = data;

        if (SlotData != null)
        {
            nicknameText.text = SlotData.NickName;
        }
        else
        {
            nicknameText.text = "Empty Slot";
        }
    }

    // 슬롯 선택 시 호출
    public void OnSlotSelected()
    {
        if (SlotData == null)
        {
            Debug.LogWarning("빈 슬롯입니다.");
        }
        else
        {
            Debug.Log($"{SlotData.NickName} 캐릭터가 선택되었습니다!");
            // 이후 로드 처리 등 추가 기능
        }
    }
}
