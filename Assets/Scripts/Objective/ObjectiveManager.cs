using UnityEngine;
using UnityEngine.UI;

public enum PlayerObjectives
{
    None,
    UanchaObjective
}
public class ObjectiveManager : MonoBehaviour
{
    public Player player;
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
            case Triggers.Trigger_1:
                objective_text.text = "Find Axe in the forest";
                break;
            case Triggers.Trigger_2:
                current_objective = PlayerObjectives.UanchaObjective;
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
    }
}
