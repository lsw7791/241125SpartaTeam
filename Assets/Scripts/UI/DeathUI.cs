using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        UIManager.Instance.deathUI = this.gameObject;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
