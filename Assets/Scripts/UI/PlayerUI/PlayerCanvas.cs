using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [Header("Name Settings")]
    public string playerName = "Player"; // 기본 플레이어 이름
    public TMP_Text playerNameText;   // 이름을 표시할 TextMeshPro

    [Header("Position Settings")]
    public Vector3 nameOffset = new Vector3(0, 1.5f, 0); // 머리 위 위치

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform; // 플레이어 Transform 가져오기

        // TextMeshPro에 이름 설정
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
    }

    private void LateUpdate()
    {
        // 이름이 플레이어 머리 위에 고정되도록 위치 업데이트
        if (playerNameText != null)
        {
            playerNameText.transform.position = playerTransform.position + nameOffset;
        }
    }

    // 외부에서 플레이어 이름 설정
    public void SetPlayerName(string newName)
    {
        playerName = newName;
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
    }
}
