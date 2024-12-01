using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseTest : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
