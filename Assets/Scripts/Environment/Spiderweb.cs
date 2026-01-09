using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spiderweb : MonoBehaviour
{
    public int web_hp = 3;
    public void CutWeb()
    {
        if (web_hp > 0)
        {
            web_hp -= 1;   
        }
    }

    void Update()
    {
        if (web_hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
