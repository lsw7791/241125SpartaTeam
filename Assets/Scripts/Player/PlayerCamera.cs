using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera mainCamera;  // 메인 카메라
    public Vector3 offset;     // 카메라와 플레이어 간의 상대적 거리
    public float smoothTime = 0.3f; // 카메라 이동 부드러움 정도를 결정하는 값 (작을수록 더 부드럽고, 커질수록 더 빠르게 이동)

    private Vector3 velocity = Vector3.zero;  // 카메라의 현재 속도
    void Start()
    {
        GameManager.Instance.player._playerCamera = this;
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 기본 offset을 설정하여 카메라와 플레이어의 상대 위치를 설정
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 0, -10);  // 기본적으로 Z는 -10으로 설정
        }
    }

    void Update()
    {
        // 목표 위치는 플레이어의 위치 + 오프셋 (Z는 고정)
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, -10);

        // 카메라를 부드럽게 목표 위치로 이동
        mainCamera.transform.position = Vector3.SmoothDamp(
            mainCamera.transform.position,  // 현재 위치
            targetPosition,                  // 목표 위치
            ref velocity,                    // 카메라의 현재 속도
            smoothTime);                     // 이동 부드러움 정도
    }
}
