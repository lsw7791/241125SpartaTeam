using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour, IManager
{

    Queue<GameObject> MonsterPool = new Queue<GameObject> ();
    Queue<GameObject> MinealPool = new Queue<GameObject>();



    public void Init()
    {      
    }
}
