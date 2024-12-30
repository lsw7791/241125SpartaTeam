using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public PlayerData SlotData { get; private set; }  // 슬롯에 대한 데이터
    [SerializeField] private TextMeshProUGUI nicknameText;  // 캐릭터 이름을 표시할 텍스트
    [SerializeField] private Button selectButton;  // 슬롯 선택 버튼

    // 슬롯 초기화 메소드
    public void InitializeSlot(PlayerData data, System.Action<PlayerData> onSelected)
    {
        // 데이터 할당
        SlotData = data;

        // SlotData가 null인 경우와 아닌 경우 처리
        if (SlotData != null)
        {
            // 슬롯에 데이터가 있으면 캐릭터 이름을 표시
            nicknameText.text = SlotData.NickName;
            selectButton.interactable = true;  // 데이터가 있으면 버튼 활성화
            Debug.Log($"슬롯 초기화 완료: {SlotData.NickName}");
        }
        else
        {
            // 데이터가 없으면 "Empty Slot" 표시
            nicknameText.text = "Empty Slot";
            selectButton.interactable = false;  // 데이터가 없으면 버튼 비활성화
            Debug.LogWarning("빈 슬롯입니다.");
        }

        // 버튼 클릭 시 OnSlotSelected 호출
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            if (SlotData != null)
            {
                onSelected?.Invoke(SlotData);  // 선택된 캐릭터 데이터 전달
            }
            else
            {
                Debug.LogWarning("빈 슬롯을 선택할 수 없습니다.");
            }
        });
    }
}
