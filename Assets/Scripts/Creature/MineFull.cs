using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFull : MonoBehaviour
{
    [SerializeField]private Mine mine;
    private void Awake()
    {
        mine = GetComponentInParent<Mine>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Weapon"))
        {
            // Weapon 스크립트에서 id 값을 가져오기
            PlayerWeapon playerWeapon = collision.collider.GetComponent<PlayerWeapon>();

            // 데미지를 받는 메서드 호출
            mine.TakeDamage(GameManager.Instance.Player.Stats.MineDamage);
        }
    }
    public void ObjectSetActive(bool temp)
    {
        gameObject.SetActive(temp);
    }
}
