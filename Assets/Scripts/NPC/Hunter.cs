using UnityEngine;
using TMPro;
using System.Collections;

public class Hunter : MonoBehaviour, IInteractable
{
    public bool hunter_quest_1;
    private TextMeshPro npc_name_hunter;
    public NPCDialog hunter_dialogue_data;
    public bool CanInteract()
    {
        return true;
    }

    public void Interact(Player player)
    {
        player.start_dialogue(hunter_dialogue_data);
    }
   
    void Awake()
    {
        npc_name_hunter = GetComponentInChildren<TextMeshPro>();
        npc_name_hunter.text = "Hunter";
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hunter_quest_1 == true)
            return;

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (player.gold_currency < 7)
                return;
                
            if (player.gold_currency >= 7)
            {
                player.gold_currency -= 7;
                hunter_quest_1 = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/Bow"), transform.position + Vector3.down, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Trigger/Trigger_hunter_bow"));
            }
        }
    }

}
