using UnityEngine;
using TMPro;
using System.Linq;

// TODO: Help
public class CheatMenu : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_InputField cheat_input;
    public void CheatCode()
    {
        string last_cheat_input = cheat_input.text;

        // TODO: Switch

        if (last_cheat_input.StartsWith("change_movement"))
        {
            last_cheat_input = last_cheat_input.Split('_')[^1];
            bool last_cheat_bool = int.TryParse(last_cheat_input, out int local_value);

            // TODO: Cheat menu error
            if (last_cheat_bool != true)
                return;
            
            player.Change_Movement = local_value;
        }
        else if (last_cheat_input.StartsWith("insert_weapon"))
        {
            if (player.last_interactable_object != null)
                return;

            string[] list_of_weapons = {"Axe", "Bow", "Pickaxe", "Sword"};
            string weapon = last_cheat_input.Split('_')[^1];

            // TODO: Cheat menu error
            if (!list_of_weapons.Contains(weapon))
                return;
            
            GameObject weapons_prefab = Resources.Load<GameObject>("Prefabs/" + weapon);
            Instantiate(weapons_prefab, player.Player_Position, Quaternion.Euler(0,0,0));
        }
    }
}
