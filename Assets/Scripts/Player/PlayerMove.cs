using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private float speed = 5f;   // �̵� �ӵ�
    public Vector2 moveInput; // �̵� �Է°�
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        speed = GameManager.Instance.Player.stats.MoveSpeed;
    }
    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed; // �̵� ó��
    }
}
