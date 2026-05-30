using UnityEngine;

public class Hunter : NPC
{
    private int quest1_gold_requirements;
    void Start()
    {
        objective_manager.hunter_quest_1 = Quest_state.INVISIBLE;
        npc_name.text = "Hunter";
        quest1_gold_requirements = 7;
    }
    public override void Interact(Player player)
    {
        if (objective_manager.hunter_quest_1 == Quest_state.NOT_STARTED)
        {
            player.start_dialogue(npc_dialogue_data_1);
            objective_manager.hunter_quest_1 = Quest_state.STARTED;
            objective_manager.set_objective_text("Find pickaxe in the forest");
        }
        else if (objective_manager.hunter_quest_1 == Quest_state.STARTED || objective_manager.hunter_quest_1 == Quest_state.UPDATE_1)
        {
            player.start_dialogue(npc_dialogue_data_2);
        }
        else if (objective_manager.hunter_quest_1 == Quest_state.UPDATE_2)
        {
            if (player.GetCurrency(2) < quest1_gold_requirements)
                return;

            player.start_dialogue(npc_dialogue_data_3);
            player.UpdateCurrency(2, (-1) * quest1_gold_requirements);
            Instantiate(Resources.Load<GameObject>("Prefabs/Bow"), transform.position + Vector3.down, Quaternion.identity);
            objective_manager.hunter_quest_1 = Quest_state.FINISHED;
            objective_manager.set_objective_text("Use bow to lower down the bridge");
        }
    }
    void Update()
    {
        switch(objective_manager.hunter_quest_1)
        {
            case Quest_state.STARTED:
                if (objective_manager.get_player_melee_weapon_type() == MeleeType.Tool_Pickaxe)
                {
                    objective_manager.hunter_quest_1 = Quest_state.UPDATE_1;
                }
                break;
            case Quest_state.UPDATE_1:
                if (objective_manager.player.GetCurrency(2) >= quest1_gold_requirements)
                {
                    objective_manager.hunter_quest_1 = Quest_state.UPDATE_2;
                }

                if (objective_manager.get_player_melee_weapon_type() == MeleeType.Tool_Pickaxe)
                {
                    objective_manager.set_objective_text("Mine gold: " + objective_manager.player.GetCurrency(2).ToString() + "/" + quest1_gold_requirements.ToString());
                }
                else
                {
                    objective_manager.set_objective_text("Pickup the Pickaxe");
                }   
                break;
            case Quest_state.UPDATE_2:
                objective_manager.set_objective_text("Go back to the Hunter");
                break;
        }
    }
}