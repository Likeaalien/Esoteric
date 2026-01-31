using UnityEngine;
public enum Triggers
{
    Start_trigger,
    Uancha_quest_trigger,
    Axe_trigger,
    Uancha_quest_done,
    Hunter_quest_trigger,
    Wooden_door_key_trigger
}
public class ObjectiveTrigger : MonoBehaviour
{
    public Triggers last_trigger; 
}
