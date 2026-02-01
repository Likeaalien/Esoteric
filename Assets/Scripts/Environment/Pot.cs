using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public bool easter_egg;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (easter_egg != false)
            return;

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (player.gold_currency >= 7 && player.ore_currency >= 7)
            {
                player.gold_currency -= 7;
                player.ore_currency -= 7;
                easter_egg = true;
                Destroy(gameObject);
                Instantiate(Resources.Load<GameObject>("Prefabs/Filled_pot"));
                Instantiate(Resources.Load<GameObject>("Prefabs/first_symbol"));
                Instantiate(Resources.Load<GameObject>("Prefabs/second_symbol"));
                Instantiate(Resources.Load<GameObject>("Prefabs/third_symbol"));
                Instantiate(Resources.Load<GameObject>("Prefabs/fourth_symbol"));
            }
        }
    }
    
}
