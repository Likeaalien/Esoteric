using UnityEngine;

public enum Triggers
{
    Start_trigger,
    Trigger_1,
    Trigger_2
}
public class ObjectiveTrigger : MonoBehaviour
{
    public Triggers last_trigger; 
}
