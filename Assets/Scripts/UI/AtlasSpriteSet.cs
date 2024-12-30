using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Image 컴포넌트를 사용하기 위해 필요한 네임스페이스
public class AtlasSpriteSet : MonoBehaviour
{
    public int UGSNumber;
    void Start()
    {
        // Image 컴포넌트 가져오기
        Image imageComponent = GetComponent<Image>();

            // Source Image 변경
            imageComponent.sprite = UIManager.Instance.UIAtlas.GetSprite(GameManager.Instance.DataManager.UIDataManager.GetAtlasData(UGSNumber));

    }
}
