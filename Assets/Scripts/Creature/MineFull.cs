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
            // Weapon ��ũ��Ʈ���� id ���� ��������
            PlayerWeapon playerWeapon = collision.collider.GetComponent<PlayerWeapon>();

            // �������� �޴� �޼��� ȣ��
            mine.TakeDamage(GameManager.Instance.Player.Stats.MineDamage);
        }
    }
    public void ObjectSetActive(bool temp)
    {
        gameObject.SetActive(temp);
    }
}
