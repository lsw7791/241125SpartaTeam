using Constants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _useMenuName;
    
    public void UseType(string inName)
    {
        _useMenuName.text = inName;
    }
}
