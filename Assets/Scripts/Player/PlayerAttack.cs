using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Player playerCurrent;

    private void Awake()
    {
        playerCurrent = GetComponent<Player>();
    }
}
