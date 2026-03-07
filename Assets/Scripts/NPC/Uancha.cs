using UnityEngine;

public class Uancha : NPC
{
    public bool uancha_Quest_1_is_completed;
    void Start()
    {
        npc_name.text = "Uancha";
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(uancha_Quest_1_is_completed)
            return;

        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            if (player.wood_currency < 5)
                return;

            if (player.wood_currency >= 5)
            {
                player.wood_currency -= 5;
                uancha_Quest_1_is_completed = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_key"), transform.position + Vector3.left, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Trigger/Trigger_Uancha_2")); 
            }
        }
    }
}