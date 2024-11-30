using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseTest : MonoBehaviour
{
    public Canvas canvas;


    public void Hide()
    {
        UIManagerTest.Instance.Hide(gameObject.name);
    }
}
