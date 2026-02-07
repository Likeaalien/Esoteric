using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Uancha : MonoBehaviour, IInteractable
{
    [SerializeField] private DialoguePanel dialogue_panel;
    public NPCDialog dialogue_data;
    private int dialogue_index;
    public bool is_typing, is_dialogue_active; 
    public bool Uancha_Quest_1_is_completed;
    private TextMeshPro npc_name_1;
    
    void Awake()
    {
        npc_name_1 = GetComponentInChildren<TextMeshPro>();
        npc_name_1.text = "Uancha";
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Uancha_Quest_1_is_completed)
            return;

        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            if (player.wood_currency < 5)
                return;

            if (player.wood_currency >= 5)
            {
                player.wood_currency -= 5;
                Uancha_Quest_1_is_completed = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_key"), transform.position + Vector3.left, Quaternion.identity);
                Instantiate(Resources.Load<GameObject>("Trigger/Trigger_Uancha_2")); 
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
