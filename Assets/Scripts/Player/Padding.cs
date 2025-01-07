using UnityEngine;
using UnityEngine.UI;  // Image 사용을 위해 필요

public class Padding : MonoBehaviour
{
    //private Image _shieldImage;  // UI에서 Image를 사용할 경우

    //private void Start()
    //{
    //    // 이미 GameManager를 통해 _shieldSlot의 Image를 참조할 수 있다면 Start에서 따로 처리할 필요는 없음
    //    _shieldImage = GameManager.Instance.Player.equipment._equipmentUI._shieldSlot.GetComponent<Image>();  // shieldSlot의 Image 컴포넌트 참조

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
    //    // GameManager.Instance.Player.equipment._equipmentUI._shieldSlot.sprite가 정상적으로 참조되는지 확인
    //    Sprite shieldSprite = GameManager.Instance.Player.equipment._equipmentUI._shieldSlot.sprite;

    //    if (_shieldImage != null)
    //    {
    //        if (shieldSprite != null)
    //        {
    //            // UI Image 컴포넌트를 통해 스프라이트를 설정
    //            _shieldImage.sprite = shieldSprite;
    //            Debug.Log("Image: Sprite set successfully.");
    //            Debug.Log($"이미지 {_shieldImage.sprite.ToString()}");
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