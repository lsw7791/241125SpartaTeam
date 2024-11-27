using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class TopDownController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    public event Action<Vector2> OnLookEvent;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Camera camera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // 이동
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // OnLookEvent 이벤트 호출 대신 SendMessage를 사용하여 호출
    public void CallLookEvent(Vector2 direction)
    {
        SendMessage("OnAim", direction);  // SendMessage로 OnAim 호출
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * speed;
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);  // SendMessage 방식으로 OnAim 호출
    }
}