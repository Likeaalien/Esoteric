using UnityEngine;
using UnityEngine.UI;

public enum PlayerObjectives
{
    None,
    UanchaObjective,
    HunterObjective,
}
public class ObjectiveManager : MonoBehaviour
{
    public Player player;
    public Uancha Uancha_NPC;
    public Text objective_text;
    PlayerObjectives current_objective = PlayerObjectives.None;
    int uancha_objective_amount = 5;
    int hunter_objective_amount = 7;
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
                objective_text.text = "Use the key to unlock the door";    
                break;
            case Triggers.Find_hunter_quest_trigger:
                objective_text.text = "Find the hunter";
                break;
            case Triggers.Hunter_find_pickaxe:
                objective_text.text = "Find pickaxe in the forest";
                break; 
            case Triggers.Hunter_mine_gold:
                current_objective = PlayerObjectives.HunterObjective;
                break;
            case Triggers.Hunter_quest_done:
                objective_text.text = "Use bow to lower down the bridge";
                break;
            case Triggers.Bridge_done:
                objective_text.text = "Congratulations! You've been added to our leaderboard.";
                player.SendLeaderboardData();
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
        if(current_objective == PlayerObjectives.HunterObjective)
        {
            objective_text.text = "Mine gold: " + player.gold_currency.ToString() + "/" + hunter_objective_amount.ToString(); 
            if(player.gold_currency >= hunter_objective_amount)
            {
                objective_text.text = "Go back to the Hunter";
                current_objective = PlayerObjectives.None;
            }    
        }
    }
}
