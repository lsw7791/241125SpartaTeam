using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFull : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    public void ObjectSetActive(bool temp)
    {
        gameObject.SetActive(temp);
    }
}
