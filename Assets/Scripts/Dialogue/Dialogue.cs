using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string speakerName;

    [TextArea(3, 8)]
    public string[] sentences;
}
