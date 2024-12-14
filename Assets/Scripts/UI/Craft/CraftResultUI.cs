using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MainData;

public class CraftResultUI : UIBase
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Image resultImage;
    [SerializeField] private TMP_Text messageText;

    public void ShowSuccess(CraftingData data)
    {
        resultText.text = "제작에 성공하였습니다!";
        resultImage.sprite = Resources.Load<Sprite>(data.imagePath);
        messageText.text = "30초 후 자동으로 창이 닫힙니다.";
        // 아이템을 인벤토리로 이동하는 로직 필요
        // 예: 인벤토리 추가
    }

    public void ShowFailure(CraftingData data)
    {
        resultText.text = "재료가 부족하여 제작에 실패하였습니다.";
        resultImage.sprite = null;
        messageText.text = "부족한 재료를 확인해 주세요.";
    }
}
