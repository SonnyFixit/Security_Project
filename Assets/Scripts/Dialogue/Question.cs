using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
    public string question;
    [Tooltip("Nie dodawaæ liczb na pocz¹tku (zrobi to skrypt)")]
    public string[] answers;
    public string pickedAnswer;
}
