using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private float speed = 5f;   // 이동 속도
    public Vector2 moveInput; // 이동 입력값
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
        rb.velocity = moveInput * speed; // 이동 처리
    }
}
