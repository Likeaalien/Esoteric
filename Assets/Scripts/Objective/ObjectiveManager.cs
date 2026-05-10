using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum Quest_state
{
    NONE,
    INVISIBLE,
    NOT_STARTED,
    STARTED,
    UPDATE_1,
    UPDATE_2,
    FINISHED
}
public enum PlayerObjectives
{
    None,
    UanchaObjective,
    HunterObjective,
}
public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objective_text;
    public Quest_state uancha_quest_1;
    public Quest_state hunter_quest_1;
    public Player player;

    public void set_objective_text(string text)
    {
        objective_text.text = text;
    }
    public MeleeType? get_player_melee_weapon_type()
    {
        MeleeWeapon current_weapon = player.player_current_weapon as MeleeWeapon;
        if(current_weapon != null)
        {
            return current_weapon.weapon_type;
        }
        else
        {
            return null;
        }
    }

    public void HandleTriggers(ObjectiveTrigger trigger)
    {
        switch(trigger.last_trigger)
        {
            case Triggers.Start_trigger:
                set_objective_text("Talk to Uancha");
                break;
            case Triggers.Bridge_done:
                set_objective_text("Congratulations! You've been added to our leaderboard.");
                player.SendLeaderboardData();
                break;
        }
        Destroy(trigger.gameObject);
    }
}
