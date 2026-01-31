using UnityEngine;
using UnityEngine.UI;

public enum PlayerObjectives
{
    None,
    UanchaObjective,
    UanchaObjectiveDone,
    HunterObjective,
}
public class ObjectiveManager : MonoBehaviour
{
    public Player player;
    public Uancha Uancha_NPC;
    public Text objective_text;
    PlayerObjectives current_objective = PlayerObjectives.None;
    int uancha_objective_amount = 5;
    public void HandleTriggers(ObjectiveTrigger trigger)
    {
        switch(trigger.last_trigger)
        {
            case Triggers.Start_trigger:
                objective_text.text = "Talk to Uancha";
                break;
            case Triggers.Uancha_quest_trigger:
                objective_text.text = "Find Axe in the forest";
                break;
            case Triggers.Axe_trigger:
                current_objective = PlayerObjectives.UanchaObjective;
                break;
            case Triggers.Uancha_quest_done:
                current_objective = PlayerObjectives.UanchaObjectiveDone;
                break;
            case Triggers.Hunter_quest_trigger:
                current_objective = PlayerObjectives.None;
                objective_text.text = "Find the hunter";
                break;
            case Triggers.Wooden_door_key_trigger:
                objective_text.text = "Door";
                break;
        }
        Destroy(trigger.gameObject);
    }

    void Update()
    {
        if(current_objective == PlayerObjectives.UanchaObjective)
        {
            objective_text.text = "Cut wood: " + player.wood_currency.ToString() + "/" + uancha_objective_amount.ToString(); 
            if(player.wood_currency == uancha_objective_amount)
            {
                objective_text.text = "Go back to Uancha";
                current_objective = PlayerObjectives.None;
            }
        }
        if (current_objective == PlayerObjectives.UanchaObjectiveDone && Uancha_NPC.Uancha_Quest_1_is_completed == true)
        {
            objective_text.text = "Push forward";
        }
    }
}
