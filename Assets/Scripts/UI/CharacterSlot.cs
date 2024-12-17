using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public PlayerData SlotData { get; private set; }
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private Button selectButton;

    public void InitializeSlot(PlayerData data, System.Action<PlayerData> onSelected)
    {
        SlotData = data;
        nicknameText.text = SlotData != null ? SlotData.NickName : "Empty Slot";

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onSelected?.Invoke(SlotData));
    }
}
