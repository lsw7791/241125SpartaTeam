using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 5f; // 구를 거리
    public float rollSpeed = 10f; // 구르는 속도
    private Collider playerCollider; // 플레이어의 콜라이더

    public bool isRolling = false; // 구르는 중인지 여부
    private void Awake()
    {
        playerCollider = GetComponent<Collider>();
    }
    public IEnumerator Roll()
    {
        Debug.Log("Rolling Started");
        // 마우스 델타 방향 계산
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        if (mouseDelta.magnitude < 0.1f) yield break; // 마우스 델타가 너무 작은 경우 구르지 않음

        isRolling = true;

        // 캐릭터 콜라이더 비활성화
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // 구르는 방향과 속도 계산
        Vector3 rollDirection = mouseDelta.normalized; // 마우스 이동 방향
        float rollAmount = 0f; // 구른 거리 초기화
        Vector3 startPosition = transform.position;

        while (rollAmount < rollDistance)
        {
            Debug.Log("Rolling");
            // 이동 진행
            float distanceThisFrame = rollSpeed * Time.deltaTime;
            transform.position += rollDirection * distanceThisFrame;
            rollAmount += distanceThisFrame;

            yield return null; // 다음 프레임까지 기다림
        }
        Debug.Log("Rolling1");

        // 구르기 끝나면 콜라이더를 다시 활성화
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        isRolling = false; // 구르기 완료
        Debug.Log("Rolling Ended");

    }
}