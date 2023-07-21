using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(3,10)] //Default size the textbox in the inspector will be
    public string[] sentences; // Allows for a custom text-string list to be created in the inspector that can be added in size
}
