using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [Header("Name Settings")]
    public string playerName = "Player"; // �⺻ �÷��̾� �̸�
    public TMP_Text playerNameText;   // �̸��� ǥ���� TextMeshPro

    [Header("Position Settings")]
    public Vector3 nameOffset = new Vector3(0, 1.5f, 0); // �Ӹ� �� ��ġ

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform; // �÷��̾� Transform ��������

        // TextMeshPro�� �̸� ����
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
    }

    private void LateUpdate()
    {
        // �̸��� �÷��̾� �Ӹ� ���� �����ǵ��� ��ġ ������Ʈ
        if (playerNameText != null)
        {
            playerNameText.transform.position = playerTransform.position + nameOffset;
        }
    }

    // �ܺο��� �÷��̾� �̸� ����
    public void SetPlayerName(string newName)
    {
        playerName = newName;
        if (playerNameText != null)
        {
            playerNameText.text = playerName;
        }
    }
}
