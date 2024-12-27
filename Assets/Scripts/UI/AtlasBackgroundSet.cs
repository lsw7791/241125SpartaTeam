using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AtlasBackgroundSet : MonoBehaviour
{
    public int UGSNumber;
    void Start()
    {
        // Image 컴포넌트 가져오기
        Image imageComponent = GetComponent<Image>();

            // Source Image 변경
            imageComponent.sprite = UIManager.Instance.BackgroundAtlas.GetSprite(GameManager.Instance.DataManager.UIDataManager.GetAtlasData(UGSNumber));

    }
}
