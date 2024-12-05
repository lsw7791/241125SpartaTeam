using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVisuals : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void AddVisual(string spritevalue,string controllervalue)
    {
       
        spriteRenderer = Resources.Load<SpriteRenderer>(spritevalue);
        animator = Resources.Load<Animator>(controllervalue);
    }
}
