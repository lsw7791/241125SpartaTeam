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
        resultText.text = "���ۿ� �����Ͽ����ϴ�!";
        resultImage.sprite = Resources.Load<Sprite>(data.imagePath);
        messageText.text = "30�� �� �ڵ����� â�� �����ϴ�.";
        // �������� �κ��丮�� �̵��ϴ� ���� �ʿ�
        // ��: �κ��丮 �߰�
    }

    public void ShowFailure(CraftingData data)
    {
        resultText.text = "��ᰡ �����Ͽ� ���ۿ� �����Ͽ����ϴ�.";
        resultImage.sprite = null;
        messageText.text = "������ ��Ḧ Ȯ���� �ּ���.";
    }
}
