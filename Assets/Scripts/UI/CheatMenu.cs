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
        string [] split_input = last_cheat_input.Split('_');

        string parameter1;
        string parameter2;

        // TODO: Switch
        if (last_cheat_input.StartsWith("change_movement"))
        {
            parameter1 = split_input[^1];

            bool success = int.TryParse(parameter1, out int local_value);

            // TODO: Cheat menu error
            if (success != true)
                return;
            
            player.Change_Movement = local_value;
        }
        else if (last_cheat_input.StartsWith("change_value"))
        {
            parameter1 = split_input[^2];
            parameter2 = split_input[^1];

            bool convert_to_id = int.TryParse(parameter1, out int id);
            bool convert_to_number = int.TryParse(parameter2, out int value);

            // TODO: Cheat menu error
            if (convert_to_number != true || convert_to_id != true)
                return;

            player.UpdateCurrency(id, value);
        }
        else if (last_cheat_input.StartsWith("insert_weapon"))
        {
            parameter1 = split_input[^1];

            if (player.last_interactable_object != null)
                return;

            string[] list_of_weapons = {"Axe", "Bow", "Pickaxe", "Sword"};

            // TODO: Cheat menu error
            if (!list_of_weapons.Contains(parameter1))
                return;
            
            GameObject weapons_prefab = Resources.Load<GameObject>("Prefabs/" + parameter1);
            Instantiate(weapons_prefab, player.Player_Position, Quaternion.Euler(0,0,0));
        }
    }
}
