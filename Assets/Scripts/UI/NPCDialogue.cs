using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class that you can create as a new object which will hold different types of data for me
[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "NPC Dialogue")]
public class NPCDialog : ScriptableObject
{
    public string npc_name;
    public Sprite npc_portrait;
    public string[] dialogue_lines;
}
