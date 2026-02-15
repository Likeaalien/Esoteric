using UnityEngine;
using TMPro;
using System.Collections;

public class Hunter : MonoBehaviour, IInteractable
{
    public bool hunter_quest_1;
    private TextMeshPro npc_name;
    [SerializeField] private DialoguePanel dialogue_panel;
    public NPCDialog dialogue_data;
    private int dialogue_index;
    public bool is_typing, is_dialogue_active; 
    
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
                Instantiate(Resources.Load<GameObject>("Prefabs/Bow"), transform.position + Vector3.down, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Trigger/Trigger_hunter_bow"));
            }
        }
    }
    public bool CanInteract()
    {
        return !is_dialogue_active;   
    }
    public void Interact()
    {
        if (dialogue_data == null)
            return;
    
        StartDialogue();
    }
    void StartDialogue()
    {
        is_dialogue_active = true;

        dialogue_index = 0;
        dialogue_panel.portrait_image.sprite = dialogue_data.npc_portrait;
        dialogue_panel.name_text.SetText(dialogue_data.npc_name);
        dialogue_panel.gameObject.SetActive(true);

        NextLine();
    }
    void NextLine()
    {       
        if (dialogue_index < dialogue_data.dialogue_lines.Length)
        {
            StartCoroutine(TypeLine());
            dialogue_index++;
        }
        else
        {
            EndDialogue();
        }
    }
    IEnumerator TypeLine()
    {
        dialogue_panel.dialogue_text.SetText("");

        foreach(char letter in dialogue_data.dialogue_lines[dialogue_index])
        {
            dialogue_panel.dialogue_text.text += letter;
            yield return new WaitForSeconds(dialogue_data.typing_speed);
        }

        yield return new WaitForSeconds(dialogue_data.auto_progress_delay);
        NextLine();
    }
   
    public void EndDialogue()
    {
        StopAllCoroutines();
        dialogue_panel.dialogue_text.SetText("");
        dialogue_panel.gameObject.SetActive(false);
        is_dialogue_active = false;
    }
}
