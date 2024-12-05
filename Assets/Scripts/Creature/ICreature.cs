using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreature
{
    public void ResetStatus();
    public void DropItems();
    void TakeDamage(int damage);
    void Die();
}
