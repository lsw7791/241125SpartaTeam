using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 moveInput; // �̵� �Է°�
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
  
    private void FixedUpdate()
    {
        rb.velocity = moveInput * GameManager.Instance.Player.stats.MoveSpeed;  // �̵� ó��
    }
}
