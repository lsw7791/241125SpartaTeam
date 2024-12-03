using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ
    private Collider2D _weaponCollider;
    [SerializeField] GameObject[] _attackObjects; // 0��: ȭ��, 1��: ���̾
    [SerializeField] RectTransform _aoSpawnPoint; // �߻� ��ġ

    private void Awake()
    {
        _weapon = this.gameObject;
        EquipManager.Instance.WeaponObject = _weapon;
        _weaponCollider = _weapon.GetComponent<Collider2D>();  // ������ Collider2D ��������
        _weaponCollider.enabled = false;  // ���� �� �ݶ��̴� ��Ȱ��ȭ
    }

    // ���� �� ������ �ݶ��̴��� Ȱ��ȭ�ϴ� �޼���
    public void ActivateWeaponCollider()
    {
        _weaponCollider.enabled = true;  // ���� �� �ݶ��̴� Ȱ��ȭ
        Debug.Log("Weapon Collider Activated!");
    }

    // ���� �ð� �� ������ �ݶ��̴��� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    public IEnumerator DisableColliderAfterAttack()
    {
        yield return new WaitForSeconds(0.2f);  // ���� �� ��� ��ٸ�
        _weaponCollider.enabled = false;  // �ݶ��̴� ��Ȱ��ȭ
        Debug.Log("Weapon Collider Deactivated!");
    }

    // ȭ�� �߻�
    public void FireArrow()
    {
        if (_attackObjects.Length > 0 && _attackObjects[0] != null)
        {
            GameObject arrow = Instantiate(_attackObjects[0], _aoSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _aoSpawnPoint.position).normalized;
            arrow.GetComponent<Rigidbody2D>().velocity = direction * 10f;  // �ӵ��� ������ ����
            Debug.Log("Fired Arrow!");
        }
    }

    // ���̾ �߻�
    public void FireFireball()
    {
        if (_attackObjects.Length > 1 && _attackObjects[1] != null)
        {
            GameObject fireball = Instantiate(_attackObjects[1], _aoSpawnPoint.position, Quaternion.identity);
            Vector3 direction = (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _aoSpawnPoint.position).normalized;
            fireball.GetComponent<Rigidbody2D>().velocity = direction * 5f;  // �ӵ��� ������ ����
            Debug.Log("Fired Fireball!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // �÷��̾ �浹�� ������Ʈ�� IDamageable�� ������ ���
            damageable.TakeDamage(_player.Stats.Damage);  // ������ ó��
            Debug.Log($"Player dealt {_player.Stats.Damage} damage to {damageable.GetType().Name}");
        }
    }
}
