using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public int ATKType;
    [SerializeField] private GameObject _weapon; // ���� ���⸦ ��Ÿ���� ������Ʈ
    private Collider2D _weaponCollider;
    [SerializeField] private GameObject[] _attackObjects; // 0��: ȭ��, 1��: ���̾
    [SerializeField] private RectTransform _aoSpawnPoint; // �߻� ��ġ

    [SerializeField]
    protected LayerMask _layerMask; // ���� ���̾��ũ
    [SerializeField]
    protected LayerMask _obstacleLayer; //  ��ֹ� ���̾��ũ

    private void Start()
    {
        //GameManager.Instance.Player._playerWeapon = this;
        _weapon = this.gameObject;
        GameManager.Instance.Player.weaponObject = _weapon;
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
        GameObject arrow = GameManager.Instance.SpawnManager.projectilePool.SpawnFromPool("Arrow");
        if (arrow == null)
        {
            //Debug.LogError("Arrow could not be spawned from pool.");
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
        outProjectile.Initialize(_layerMask, _obstacleLayer, GameManager.Instance.Player.Stats.Damage+ GameManager.Instance.Player.Stats.PhysicalDamage);

        //Debug.Log("Fired Arrow!");
    }


    // ���̾ �߻�
    public void FireFireball()
    {
        //GameObject fireball = Instantiate(_attackObjects[1], _aoSpawnPoint.position, Quaternion.identity);
        GameObject fireball = GameManager.Instance.SpawnManager.projectilePool.SpawnFromPool("FireBall");
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
        outProjectile.Initialize(_layerMask, _obstacleLayer, GameManager.Instance.Player.Stats.Damage+ GameManager.Instance.Player.Stats.MagicalDamage);

        //Debug.Log("Fired Fireball!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �θ� null���� Ȯ��
        if (collision.transform.parent != null &&
            collision.transform.parent.TryGetComponent<ICreature>(out var outTarget))
        {
            // �÷��̾ �浹�� ������Ʈ�� ICreature�� ������ ���
            if (collision.gameObject.CompareTag("Monster"))
            {
                outTarget.TakeDamage(GameManager.Instance.Player.Stats.Damage + GameManager.Instance.Player.Stats.PhysicalDamage);  // ������ ó��
                //Debug.Log($"Player dealt {GameManager.Instance.Player.Stats.Damage + GameManager.Instance.Player.Stats.PhysicalDamage} damage to {outTarget.GetType().Name}");
            }
            else if (collision.gameObject.CompareTag("Mine"))
            {
                outTarget.TakeDamage(GameManager.Instance.Player.Stats.MineDamage);  // ������ ó��
            }

        }
    }
}
