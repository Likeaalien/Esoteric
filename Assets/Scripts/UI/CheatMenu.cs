using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using System.Collections;
using NUnit.Framework.Constraints;

// TODO: Help
public class CheatMenu : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_InputField cheat_input;
    public void CheatCode()
    {
        string last_cheat_input = cheat_input.text;

        string[] inputs = last_cheat_input.Split(' ');

        string command = inputs[0];

        switch (command)
        {
            case "change_movement":
                if (inputs.Length < 2)
                    return;

                bool success = int.TryParse(inputs[1], out int movement);
                // TODO: Cheat menu error
                if (success != true)
                    return;
                
                player.Change_Movement = movement;
                break;

            case "change_value":
                if (inputs.Length < 3)
                    return;

                bool id_success = int.TryParse(inputs[1], out int id);
                bool value_success = int.TryParse(inputs[2], out int value);

                if (id_success != true || value_success != true)
                    return;

                player.UpdateCurrency(id, value);
                break;

            case "insert_weapon":
                if (inputs.Length < 2)
                    return;

                string prefab_weapon_name = inputs[1];
                GameObject weapons_prefab = Resources.Load<GameObject>("Prefabs/" + prefab_weapon_name);
                Instantiate(weapons_prefab, player.Player_Position, Quaternion.Euler(0,0,0));
                break;
        }
    }
}
