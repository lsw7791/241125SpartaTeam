using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [Header("Name Settings")]
    public string playerName = "��"; // �⺻ �÷��̾� �̸�
    public TMP_Text playerNameText;   // �̸��� ǥ���� TextMeshPro

    //[Header("Position Settings")]
    //public Vector3 nameOffset = new Vector3(0, 1.5f, 0); // �Ӹ� �� ��ġ

    //private Transform playerTransform;

    private void Start()
    {
        //playerName = GameManager.Instance.Player.stats.NickName;
        //playerTransform = transform; // �÷��̾� Transform ��������
        //// TextMeshPro�� �̸� ����
        //playerNameText.text = playerName;
    }

    //private void OnEnable()
    //{
    //    Debug.Log("ȣ���");
    //    CharacterSelectUI selectUI = UIManager.Instance.GetUI<CharacterSelectUI>();
    //    if (selectUI.isNewGame)
    //    {
    //        playerName = GameManager.Instance.Player.stats.NickName;
    //        playerTransform = transform; // �÷��̾� Transform ��������
    //                                     // TextMeshPro�� �̸� ����
    //        playerNameText.text = playerName;
    //        playerNameText.transform.position = playerTransform.position + nameOffset;
    //    }
    //}

    //private void PlayerName()
    //{
    //    playerName = GameManager.Instance.Player.stats.NickName;
    //    playerNameText.text = playerName;
    //}


    //private void LateUpdate()
    //{
    //    playerNameText.transform.position = playerTransform.position + nameOffset;
    //}

}
