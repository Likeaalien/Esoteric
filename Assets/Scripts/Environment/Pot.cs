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
        if (player == null)
            return;
        if (player.gold_currency < 7 || player.ore_currency < 7)
            return;

        player.gold_currency -= 7;
        player.ore_currency -= 7;
        easter_egg = true;
        Destroy(gameObject);
        spawn_symbols();
        spawn_angel();
    }

    void spawn_angel()
    {
        string[] list_of_names = {"Đorđe", "Darija", "Vasilije", "Nikolina", "Matija", "Anja", "Vuk", "Nikolina", "Marko", "Andrea", "Petar",
          "Milica", "Konstantin", "Jana", "Mihajlo", "Ana", "Lazar", "Elena", "Andrej", "Nikolina", "Uroš", "Petra", "Relja"};
        int random_number = Random.Range(0, list_of_names.Length);

        GameObject angel;
        if (random_number % 2 == 0)
        {
            angel = Instantiate(Resources.Load<GameObject>("NPC/Male_angel"));
        }
        else
        {
            angel = Instantiate(Resources.Load<GameObject>("NPC/Female_angel"));
        }

        Angel angel_component = angel.GetComponent<Angel>();
        angel_component.SetName(list_of_names[random_number]);
    }

    void spawn_symbols()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Filled_pot"));
        Instantiate(Resources.Load<GameObject>("Prefabs/first_symbol"));
        Instantiate(Resources.Load<GameObject>("Prefabs/second_symbol"));
        Instantiate(Resources.Load<GameObject>("Prefabs/third_symbol"));
        Instantiate(Resources.Load<GameObject>("Prefabs/fourth_symbol"));
    }
}
