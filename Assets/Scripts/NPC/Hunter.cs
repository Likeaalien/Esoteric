using UnityEngine;
using TMPro;
using System.Collections;

public class Hunter : MonoBehaviour, IInteractable
{
    private TextMeshPro npc_name;
    private GameObject npc_interact_icon;
    public NPCDialog hunter_dialogue_data;
    public bool hunter_quest_1;
    void Awake()
    {
        npc_name = GetComponentInChildren<TextMeshPro>();
        npc_name.text = "Hunter";
    }
    public bool CanInteract()
    {
        return true;
    }
    public void CreateInteractionIcon()
    {
        if(CanInteract())
            npc_interact_icon = Instantiate(Resources.Load<GameObject>("UI/InteractionIcon"), npc_name.transform.position + new Vector3(-0.7f, 0f, 0f), Quaternion.identity);
    }
    public void Interact(Player player)
    {
        player.start_dialogue(hunter_dialogue_data);
    }
    public void DestroyInteractionIcon()
    {
        if(npc_interact_icon != null)
            Destroy(npc_interact_icon);
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
