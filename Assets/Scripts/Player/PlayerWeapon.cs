using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ
    private Collider2D _weaponCollider;
    [SerializeField] GameObject[] _attackObjects; // 0��: ȭ��, 1��: ���̾
    [SerializeField] RectTransform _aoSpawnPoint; // �߻� ��ġ

    [SerializeField]
    protected LayerMask layerMask; // ���� ���̾��ũ

    private void Start()
    {
        GameManager.Instance.player._playerWeapon = this;
        _weapon = this.gameObject;
        EquipManager.Instance.weaponObject = _weapon;
        _weaponCollider = _weapon.GetComponent<Collider2D>();  // ������ Collider2D ��������
        _weaponCollider.enabled = false;  // ���� �� �ݶ��̴� ��Ȱ��ȭ
    }

    // ���� �� ������ �ݶ��̴��� Ȱ��ȭ�ϴ� �޼���
    public void ActivateWeaponCollider()
    {
        _weaponCollider.enabled = true;  // ���� �� �ݶ��̴� Ȱ��ȭ
    }

    // ���� �ݶ��̴� ��Ȱ��ȭ
    public void DeactivateWeaponCollider()
    {
        _weaponCollider.enabled = false; // ���� ���� �� �ݶ��̴� ��Ȱ��ȭ
    }


    // ȭ�� �߻�
    public void FireArrow()
    {
        //GameObject arrow = Instantiate(_attackObjects[0], _aoSpawnPoint.position, Quaternion.identity);
        GameObject arrow = GameManager.Instance.spawnManager.projectilePool.SpawnFromPool("Arrow");
        if (arrow == null)
        {
            Debug.LogError("Arrow could not be spawned from pool.");
            return;
        }

        arrow.transform.position = _aoSpawnPoint.position;
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        // ���콺 ��ġ�� �߻� ��ġ ���� ���
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = new Vector2(mousePosition.x - _aoSpawnPoint.position.x, mousePosition.y - _aoSpawnPoint.position.y);  // Vector2�� ���

        // ���� ���͸� ����ȭ�Ͽ� ������ �ӵ��� �߻�
        direction.Normalize();  // ������ ũ�⸦ 1�� ����
        arrowRb.velocity = direction * 15f;  // ������ �ӵ��� �߻�

        // �߻�ü�� ���콺�� ���ϵ��� ȸ�� (ȭ���� �ڿ������� ���콺�� ���ϵ���)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // ���� ���
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));  // ȸ�� ����

        // �浹 ó�� - ȭ���� �浹 �� ������ ����
        arrow.TryGetComponent<ProjectileCollisionHandler>(out var outProjectile);
        outProjectile.Initialize(layerMask, GameManager.Instance.player.Stats.Damage);

        Debug.Log("Fired Arrow!");
    }


    // ���̾ �߻�
    public void FireFireball()
    {
        //GameObject fireball = Instantiate(_attackObjects[1], _aoSpawnPoint.position, Quaternion.identity);
        GameObject fireball = GameManager.Instance.spawnManager.projectilePool.SpawnFromPool("FireBall");
        if (fireball == null)
        {
            Debug.LogError("Arrow could not be spawned from pool.");
            return;
        }

        fireball.transform.position = _aoSpawnPoint.position;
        Rigidbody2D fireballRb = fireball.GetComponent<Rigidbody2D>();

        // ���콺 ��ġ�� �߻� ��ġ ���� ���
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = new Vector2(mousePosition.x - _aoSpawnPoint.position.x, mousePosition.y - _aoSpawnPoint.position.y);  // Vector2�� ���

        // ���� ���͸� ����ȭ�Ͽ� ������ �ӵ��� �߻�
        direction.Normalize();  // ������ ũ�⸦ 1�� ����
        fireballRb.velocity = direction * 15f;  // ������ �ӵ��� �߻�

        // �߻�ü�� ���콺�� ���ϵ��� ȸ�� (���̾�� �ڿ������� ���콺�� ���ϵ���)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // ���� ���
        fireball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));  // ȸ�� ����

        // �浹 ó�� - ���̾�� �浹 �� ������ ����
        fireball.TryGetComponent<ProjectileCollisionHandler>(out var outProjectile);
        outProjectile.Initialize(layerMask, GameManager.Instance.player.Stats.Damage);

        Debug.Log("Fired Fireball!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.parent.TryGetComponent<ICreature>(out var outTarget))
        {
            // �÷��̾ �浹�� ������Ʈ�� IDamageable�� ������ ���
            outTarget.TakeDamage(GameManager.Instance.player.Stats.Damage);  // ������ ó��
            Debug.Log($"Player dealt {GameManager.Instance.player.Stats.Damage} damage to {outTarget.GetType().Name}");
        }
    }
}
