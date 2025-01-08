using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [Header("Name Settings")]
    public string playerName = "▼"; // 기본 플레이어 이름
    public TMP_Text playerNameText;   // 이름을 표시할 TextMeshPro

    //[Header("Position Settings")]
    //public Vector3 nameOffset = new Vector3(0, 1.5f, 0); // 머리 위 위치

    //private Transform playerTransform;

    private void Start()
    {
        //playerName = GameManager.Instance.Player.stats.NickName;
        //playerTransform = transform; // 플레이어 Transform 가져오기
        //// TextMeshPro에 이름 설정
        //playerNameText.text = playerName;
    }

    //private void OnEnable()
    //{
    //    Debug.Log("호출됨");
    //    CharacterSelectUI selectUI = UIManager.Instance.GetUI<CharacterSelectUI>();
    //    if (selectUI.isNewGame)
    //    {
    //        playerName = GameManager.Instance.Player.stats.NickName;
    //        playerTransform = transform; // 플레이어 Transform 가져오기
    //                                     // TextMeshPro에 이름 설정
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
