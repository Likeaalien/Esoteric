using UnityEngine;

public class Uancha : NPC
{
    int quest1_wood_requirement;
    void Start()
    {
        npc_name.text = "Uancha";
        quest1_wood_requirement = 5;

        objective_manager.uancha_quest_1 = Quest_state.NOT_STARTED;
    }
    public override void Interact(Player player)
    {
        if(objective_manager.uancha_quest_1 == Quest_state.NOT_STARTED)
        {
            player.start_dialogue(npc_dialogue_data_1);
            objective_manager.uancha_quest_1 = Quest_state.STARTED;
            objective_manager.set_objective_text("Find Axe in the forest");
        }
        else if (objective_manager.uancha_quest_1 == Quest_state.STARTED || objective_manager.uancha_quest_1 == Quest_state.UPDATE_1)
        {
            player.start_dialogue(npc_dialogue_data_2);
        }
        else if (objective_manager.uancha_quest_1 == Quest_state.UPDATE_2)
        {
            if (player.GetCurrency(0) < quest1_wood_requirement)
                return;

            player.start_dialogue(npc_dialogue_data_3);
            player.UpdateCurrency(0, (-1) * quest1_wood_requirement);
            Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_key"), transform.position + Vector3.left, Quaternion.identity);
            objective_manager.uancha_quest_1 = Quest_state.FINISHED;
            objective_manager.hunter_quest_1 = Quest_state.NOT_STARTED;
            objective_manager.set_objective_text("Find the door and unlock it");
        }
    }

    void Update()
    {   
        switch (objective_manager.uancha_quest_1)
        {
            case Quest_state.STARTED:
                
                if (objective_manager.player.equipped_weapon_prefab != null)
                {
                    objective_manager.uancha_quest_1 = Quest_state.UPDATE_1;
                }
                break;
            case Quest_state.UPDATE_1:
                if (objective_manager.player.GetCurrency(0) >= quest1_wood_requirement)
                {
                    objective_manager.uancha_quest_1 = Quest_state.UPDATE_2;
                }
                if (objective_manager.get_player_melee_weapon_type() == MeleeType.Tool_Axe)
                {
                    objective_manager.set_objective_text("Cut wood: " + objective_manager.player.GetCurrency(0).ToString() + "/" + quest1_wood_requirement.ToString());
                }
                else
                {
                    objective_manager.set_objective_text("Pickup the Axe");
                }
                break;
            case Quest_state.UPDATE_2:
                objective_manager.set_objective_text("Go back to Uancha");
                break;
        }
    }
}