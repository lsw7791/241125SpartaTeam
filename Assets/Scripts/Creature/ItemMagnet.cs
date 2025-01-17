using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    private float _speed;
    private float _magnetDistance;

    private Transform _TatgetPosition;

    private TestItem _item;

    private void OnEnable()
    {
        _TatgetPosition = GameManager.Instance.Player.transform;
        _item = GetComponent<TestItem>();
    }

    private void Start()
    {
        _speed = 2f;
        _magnetDistance = 5f;
    }

    private void OnDestroy()
    {
        // 맵을 넘어갈 때 혹시 모를 버그를 위해 초기화
        _TatgetPosition = null;
    }

    private void Update()
    {
        if (!_TatgetPosition || _item.isPlayerDrop)
        { // 타겟이 초기화 되어있다면 리턴
            return;
        }

        float targetToDistance = Vector3.Distance(transform.position, _TatgetPosition.position);

        if(targetToDistance <= _magnetDistance)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, _TatgetPosition.position, _speed * Time.deltaTime);
        }
    }
}
