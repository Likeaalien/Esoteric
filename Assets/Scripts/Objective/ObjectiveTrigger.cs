using UnityEngine;
public enum Triggers
{
    Start_trigger,
    Bridge_done
}
public class ObjectiveTrigger : MonoBehaviour
{
    public Triggers last_trigger; 
}
