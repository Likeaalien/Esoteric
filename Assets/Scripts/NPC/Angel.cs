using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Angel : MonoBehaviour
{
    public TextMeshPro angel_name;

    public void SetName(string name)
    {
        angel_name.text = name;
    }
    
    void Awake()
    {
        if (angel_name == null)
            angel_name = GetComponentInChildren<TextMeshPro>();
    }
}
