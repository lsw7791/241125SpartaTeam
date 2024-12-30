using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AtlasBackgroundSet : MonoBehaviour
{
    public int UGSNumber;
    void Start()
    {
        // Image ������Ʈ ��������
        Image imageComponent = GetComponent<Image>();

            // Source Image ����
            imageComponent.sprite = UIManager.Instance.BackgroundAtlas.GetSprite(GameManager.Instance.DataManager.UIDataManager.GetAtlasData(UGSNumber));

    }
}
