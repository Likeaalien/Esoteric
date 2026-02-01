using UnityEngine;
using TMPro;

public class Hunter : MonoBehaviour
{
    public bool hunter_quest_1;

    private TextMeshPro npc_name;
    
    
    void Awake()
    {
        npc_name = GetComponentInChildren<TextMeshPro>();
  
        npc_name.text = "Hunter";
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
                Instantiate(Resources.Load<GameObject>("Prefabs/Bow"), transform.position + 2 * Vector3.down, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Trigger/Trigger_hunter_bow"));
            }
        }
    }
}
