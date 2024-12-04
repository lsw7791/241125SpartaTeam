using MainData;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;  // ������ �̹���

    // ���Կ� ������ ������ �����ϴ� �޼ҵ�
    public void Setup(CraftingData data)
    {
        // �̹��� ��η� Sprite �ε�
        Sprite sprite = Resources.Load<Sprite>(data.imagePath);
        if (sprite != null)
        {
            itemImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("������ �̹��� �ε� ����: " + data.imagePath);
        }
    }
}