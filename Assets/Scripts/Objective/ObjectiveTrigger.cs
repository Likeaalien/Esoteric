using UnityEngine;
public enum Triggers
{
    Start_trigger,
    Uancha_quest_trigger,
    Axe_trigger,
    Uancha_quest_done,
    Find_hunter_quest_trigger,
    Hunter_find_pickaxe,
    Hunter_mine_gold,
    Hunter_quest_done,
    Bridge_done
}
public class ObjectiveTrigger : MonoBehaviour
{
    public Triggers last_trigger; 
}
