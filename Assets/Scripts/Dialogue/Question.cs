using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
    public string question;
    [Tooltip("Nie dodawa� liczb na pocz�tku (zrobi to skrypt)")]
    public string[] answers;
    public string pickedAnswer;
}
