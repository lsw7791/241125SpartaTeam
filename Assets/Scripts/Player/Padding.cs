using UnityEngine;
using UnityEngine.UI;  // Image ����� ���� �ʿ�

public class Padding : MonoBehaviour
{
    //private Image _shieldImage;  // UI���� Image�� ����� ���

    //private void Start()
    //{
    //    // �̹� GameManager�� ���� _shieldSlot�� Image�� ������ �� �ִٸ� Start���� ���� ó���� �ʿ�� ����
    //    _shieldImage = GameManager.Instance.Player.equipment._equipmentUI._shieldSlot.GetComponent<Image>();  // shieldSlot�� Image ������Ʈ ����

    //    if (_shieldImage == null)
    //    {
    //        Debug.LogError("Start: _shieldImage is null. Please make sure _shieldSlot has an Image component.");
    //    }
    //    else
    //    {
    //        Debug.Log("Start: _shieldImage successfully found.");
    //    }
    //}

    //public void InsertSprite()
    //{
    //    // GameManager.Instance.Player.equipment._equipmentUI._shieldSlot.sprite�� ���������� �����Ǵ��� Ȯ��
    //    Sprite shieldSprite = GameManager.Instance.Player.equipment._equipmentUI._shieldSlot.sprite;

    //    if (_shieldImage != null)
    //    {
    //        if (shieldSprite != null)
    //        {
    //            // UI Image ������Ʈ�� ���� ��������Ʈ�� ����
    //            _shieldImage.sprite = shieldSprite;
    //            Debug.Log("Image: Sprite set successfully.");
    //            Debug.Log($"�̹��� {_shieldImage.sprite.ToString()}");
    //        }
    //        else
    //        {
    //            Debug.LogWarning("No valid sprite to set.");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No valid Image found to set the sprite.");
    //    }
    //}
}