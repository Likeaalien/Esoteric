using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uancha : MonoBehaviour
{
    private bool Uancha_Quest_1_is_completed;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Uancha_Quest_1_is_completed)
            return;

        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            if (player.wood_currency < 5)
                return;

            if (player.wood_currency >= 5)
            {
                player.wood_currency -= 5;
                Uancha_Quest_1_is_completed = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/Sword"), transform.position + 2 * Vector3.left, Quaternion.identity); 
            }
        }
    }
}
