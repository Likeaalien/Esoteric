using TMPro;
using UnityEngine;
using System.Collections;
using System;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialoguePanel dialogue_panel;
    public NPCDialog dialogue_data;
    private int dialogue_index;
    public bool is_typing, is_dialogue_active; 
    public float auto_progress_delay = 1.5f;
    public float typing_speed = 0.05f;

    public void StartDialogue()
    {
        is_dialogue_active = true;

        dialogue_index = 0;
        dialogue_panel.name_text.SetText(dialogue_data.npc_name);
        dialogue_panel.portrait_image.sprite = dialogue_data.npc_portrait;
        dialogue_panel.gameObject.SetActive(true);

        NextLine();
    }
    public void NextLine()
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
    public IEnumerator TypeLine()
    {
        dialogue_panel.dialogue_text.SetText("");

        foreach(char letter in dialogue_data.dialogue_lines[dialogue_index])
        {
            dialogue_panel.dialogue_text.text += letter;
            yield return new WaitForSeconds(typing_speed);
        }

        yield return new WaitForSeconds(auto_progress_delay);
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
