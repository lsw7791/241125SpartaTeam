using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Image ������Ʈ�� ����ϱ� ���� �ʿ��� ���ӽ����̽�
public class AtlasSpriteSet : MonoBehaviour
{
    public int UGSNumber;
    void Start()
    {
        // Image ������Ʈ ��������
        Image imageComponent = GetComponent<Image>();

            // Source Image ����
            imageComponent.sprite = UIManager.Instance.UIAtlas.GetSprite(GameManager.Instance.DataManager.UIDataManager.GetAtlasData(UGSNumber));

    }
}
